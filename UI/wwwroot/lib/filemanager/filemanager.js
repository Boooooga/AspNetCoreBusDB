var DirectoryDescriptor = function(path) {
	var self = this;

	self.path = ko.observable(path);

	self.isExpanded = ko.observable(path === '/');

	self.children = ko.observableArray([]);

	self.toggleExpandedState = function() {
		self.isExpanded(!self.isExpanded());
	};
};

var FileDescriptor = function(name, size, lastModified, relativeUrl) {
	var self = this;

	self.name = ko.observable(name);

	self.size = ko.observable(size);

	self.lastModifiedTime = ko.observable(new Date(lastModified));

	self.relativeUrl = ko.observable(relativeUrl);

	self.formattedSize = ko.computed(function() {
		var size = self.size();
		if (size < 1024) {
			return size + ' Б';
		}
		if (size < 1024 * 1024) {
			return (size / 1024).toLocaleString(undefined, { maximumFractionDigits: 2 }) + ' КБ';
		}
		return (size / 1024 / 1024).toLocaleString(undefined, { maximumFractionDigits: 2 }) + ' МБ';
	});

	self.formattedModifiedTime = ko.computed(function () {
		return self.lastModifiedTime().toLocaleDateString() + " " + self.lastModifiedTime().toLocaleTimeString();
	});
};

var FileManager = function(fileManagerSettings) {
	var self = this;

	self.currentDirectory = ko.observable();

	self.currentFile = ko.observable();

	self.getDirectoryDescriptor = function(path) {
		return self.directoriesMap()[path];
	};

	self.directoriesMap = ko.observable({
		'/': new DirectoryDescriptor('/')
	});

	self.files = ko.observableArray();

	self.selectedFilePath = ko.observable();

	self.newFileName = ko.observable();

	self.selectedDirectoryPath = ko.observable();

	self.newDirectoryName = ko.observable();

	self.isOperationRunning = ko.observable(false);

	self.currentError = ko.observable();

	self.loadDirectories = function() {
		console.log('loading directories');
		$.get({
			url: fileManagerSettings.baseManagerUrl + '/GetSubdirectories?directory=' + encodeURIComponent('/'),
			success: function(data) {
				console.log('directories loaded');
				console.log(data);
				var result = {
					'/': new DirectoryDescriptor('/')
				};
				data.forEach(function(directoryItem) {
					console.log(directoryItem);
					if (!result.hasOwnProperty(directoryItem.relativeUrl)) {
						result[directoryItem.relativeUrl] = new DirectoryDescriptor(directoryItem.relativeUrl);
					}
					if (self.directoriesMap()[directoryItem.relativeUrl] &&
						self.directoriesMap()[directoryItem.relativeUrl].isExpanded()) {
						console.log(directoryItem.relativeUrl + ' already expanded');
						result[directoryItem.relativeUrl].isExpanded(true);
					}
					var pathComponents = directoryItem.relativeUrl.substr(1).split('/');
					var directoryName = pathComponents.pop();
					var parentPath = '/' + pathComponents.join('/');
					console.log('parent path for ' + directoryItem.relativeUrl + ' is ' + parentPath);
					if (!result.hasOwnProperty(parentPath)) {
						result[parentPath] = new DirectoryDescriptor(parentPath);
					}
					result[parentPath].children.push(directoryName);
				});
				console.log(result);
				self.directoriesMap(result);
				if (!self.currentDirectory() || !self.directoriesMap().hasOwnProperty(self.currentDirectory())) {
					console.log('resetting current directory');
					self.currentDirectory('/');
				}
				self.loadFiles();
			},
			error: function(data) {
				console.log(data);
			}
		});
	};

	self.loadFiles = function() {
		console.log('loading files for directory ' + self.currentDirectory());
		$.get({
			url: fileManagerSettings.baseManagerUrl + '/GetFiles?directory=' + encodeURIComponent(self.currentDirectory()),
			success: function(data) {
				console.log('files loaded');
				console.log(data);
				self.files(data.map(function(item) {
					return new FileDescriptor(item.name, item.size, item.lastModified, item.relativeUrl);
				}));
			},
			error: function(data) {
				console.log(data);
			}
		});
	};

	self.getDirectoryName = function(path) {
		return !path ? undefined : path === '/' ? fileManagerSettings.uploadsDirectoryPath : path.split('/').pop();
	};

	self.getFileName = function(path) {
		return !path ? undefined : path.split('/').pop();
	};

	self.getFileAbsoluteUrl = function(path) {
		return encodeURI(window.location.protocol + "//" + window.location.host + fileManagerSettings.baseSiteUrl + '/' + fileManagerSettings.uploadsDirectoryPath + path);
	};

	self.getFileRelativeUrl = function(path) {
		return encodeURI('/' + fileManagerSettings.uploadsDirectoryPath + path);
	};

	self.filesUploadUrl = ko.computed(function() {
		return fileManagerSettings.baseManagerUrl + '/UploadFile?directory=' + encodeURIComponent(self.currentDirectory());
	});

	self.setCurrentDirectory = function(path) {
		self.currentDirectory(path);
		self.loadFiles();
	};

	self.setCurrentFile = function(file) {
		console.log('file selected', file.relativeUrl());
		self.currentFile(file.relativeUrl());
	};

	self.returnCurrentFile = function() {
		var currentFilePath = self.currentFile();
		if (currentFilePath) {
			if (fileManagerSettings.environment === "TinyMce") {
				window.parent.postMessage({
					mceAction: 'processFile',
					content: {
						url: self.getFileRelativeUrl(currentFilePath),
						name: self.getFileName(currentFilePath)
					}
				}, '*');
			} else {
				window.parent.postMessage({
					type: 'SpaceApp.FileManager.FileSelected',
					content: {
						managerId: fileManagerSettings.id,
						url: self.getFileRelativeUrl(currentFilePath),
						name: self.getFileName(currentFilePath)
					}
				}, '*');
			}
			self.currentFile(undefined);
		}
	};

	self.showFileRenamePopup = function(path) {
		self.selectedFilePath(path);
		self.newFileName(self.getFileName(path));
		self.currentError(undefined);
		$("#renameFileModal").modal('show');
	};

	self.renameFile = function() {
		if (self.getFileName(self.selectedFilePath()) === self.newFileName()) {
			$("#renameFileModal").modal('hide');
			return;
		}
		self.currentError(undefined);
		self.isOperationRunning(true);
		console.log('renaming file ' + self.selectedFilePath() + ' to ' + self.newFileName());
		$.post({
			url: fileManagerSettings.baseManagerUrl + '/RenameFile?file=' + encodeURIComponent(self.selectedFilePath())
				+ '&newName=' + encodeURIComponent(self.newFileName()),
			success: function(data) {
				console.log('file renamed');
				self.isOperationRunning(false);
				self.loadFiles();
				$("#renameFileModal").modal('hide');
			},
			error: function(data) {
				console.log(data);
				self.isOperationRunning(false);
				if (data.responseText === 'InvalidName') {
					self.currentError('Имя файла содержит недопустимые символы.');
				} else if (data.responseText === 'AlreadyExists') {
					self.currentError('Файл с указанным именем уже существует.');
				} else {
					self.currentError("Возникла неизвестная ошибка.");
				}
			}
		});
	};

	self.showFileDeletePopup = function(path) {
		self.selectedFilePath(path);
		self.currentError(undefined);
		$("#deleteFileModal").modal('show');
	};

	self.deleteFile = function() {
		self.currentError(undefined);
		self.isOperationRunning(true);
		console.log('deleting file ' + self.selectedFilePath());
		$.post({
			url: fileManagerSettings.baseManagerUrl + '/DeleteFile?file=' + encodeURIComponent(self.selectedFilePath()),
			success: function(data) {
				console.log('file deleted');
				self.isOperationRunning(false);
				self.loadFiles();
				$("#deleteFileModal").modal('hide');
			},
			error: function(data) {
				console.log(data);
				self.isOperationRunning(false);
				self.currentError("Не удалось удалить файл.");
			}
		});
	};

	self.showDirectoryRenamePopup = function(path) {
		self.selectedDirectoryPath(path);
		self.newDirectoryName(self.getDirectoryName(path));
		self.currentError(undefined);
		$("#renameDirectoryModal").modal('show');
	};

	self.createDirectory = function() {
		self.currentError(undefined);
		self.isOperationRunning(true);
		var directoryPath = self.currentDirectory() + '/' + self.newDirectoryName();
		console.log('creating directory ' + directoryPath);
		$.post({
			url: fileManagerSettings.baseManagerUrl + '/CreateDirectory?directory=' + encodeURIComponent(directoryPath),
			success: function(data) {
				console.log('directory created');
				self.newDirectoryName(undefined);
				self.isOperationRunning(false);
				self.loadDirectories();
				$("#createDirectoryModal").modal('hide');
			},
			error: function(data) {
				console.log(data);
				self.isOperationRunning(false);
				self.currentError("Не удалось создать папку.");
			}
		});
	};

	self.renameDirectory = function() {
		if (self.getDirectoryName(self.selectedDirectoryPath()) === self.newDirectoryName()) {
			$("#renameDirectoryModal").modal('hide');
			return;
		}
		self.currentError(undefined);
		self.isOperationRunning(true);
		console.log('renaming directory ' + self.selectedDirectoryPath() + ' to ' + self.newDirectoryName());
		$.post({
			url: fileManagerSettings.baseManagerUrl + '/RenameDirectory?directory=' + encodeURIComponent(self.selectedDirectoryPath())
				+ '&newName=' + encodeURIComponent(self.newDirectoryName()),
			success: function(data) {
				console.log('directory renamed');
				self.isOperationRunning(false);
				//if (self.selectedDirectoryPath() === self.currentDirectory()) {
				//	var pathComponents = self.selectedDirectoryPath().split('/');
				//	pathComponents.pop();
				//	pathComponents.push(self.newDirectoryName());
				//	self.currentDirectory(pathComponents.join('/'));
				//	console.log('current directory set to ' + self.currentDirectory());
				//}
				self.loadDirectories();
				$("#renameDirectoryModal").modal('hide');
			},
			error: function(data) {
				console.log(data);
				self.isOperationRunning(false);
				if (data.responseText === "InvalidName") {
					self.currentError("Имя папки содержит недопустимые символы.");
				} else if (data.responseText === "AlreadyExists") {
					self.currentError("Папка с указанным именем уже существует.");
				} else {
					self.currentError("Возникла неизвестная ошибка.");
				}
			}
		});
	};

	self.showDirectoryDeletePopup = function(path) {
		self.selectedDirectoryPath(path);
		self.currentError(undefined);
		$("#deleteDirectoryModal").modal('show');
	};

	self.deleteDirectory = function() {
		self.currentError(undefined);
		self.isOperationRunning(true);
		console.log('deleting directory ' + self.selectedDirectoryPath());
		$.post({
			url: fileManagerSettings.baseManagerUrl + '/DeleteDirectory?directory=' + encodeURIComponent(self.selectedDirectoryPath()),
			success: function (data) {
				console.log('directory deleted');
				self.isOperationRunning(false);
				self.loadDirectories();
				$("#deleteDirectoryModal").modal('hide');
			},
			error: function(data) {
				console.log(data);
				self.isOperationRunning(false);
				self.currentError("Не удалось удалить папку.");
			}
		});
	};
};

Dropzone.autoDiscover = false;

$(document).ready(function() {
	$('body').on('dblclick', '.files-list .file-item', function () {
		ko.contextFor(this).$root.returnCurrentFile();
	});

	$("#deleteDirectoryModal, #deleteFileModal, #renameDirectoryModal, #renameFileModal, #uploadFileModal").modal({
		show: false
	}).on('hide.bs.modal', function(e) {
		if (ko.contextFor(this).$root.isOperationRunning()) {
			e.preventDefault();
		}
	});

	$('#filesUploadForm').dropzone({
		url: function() {
			return ko.contextFor($('#filesUploadForm')[0]).$root.filesUploadUrl();
		},
		maxFilesize: 30,
		previewTemplate: document.querySelector('#fileItemTemplate').innerHTML,
		dictDefaultMessage: 'Нажмите, чтобы выбрать файл.',
		dictFileTooBig: 'Разрешена загрузка файлов размером не более {{maxFilesize}} МБ.',
		dictInvalidFileType: 'Загрузка файлов данного типа запрещена',
		dictResponseError: 'Не удалось загрузить файл (код ответа {{statusCode}})',
		dictFileSizeUnits: { tb: "ТБ", gb: "ГБ", mb: "МБ", kb: "КБ", b: "б" },
		init: function() {
			this.on("addedfile", function (file) {
				var rootModel = ko.contextFor($('#filesUploadForm')[0]).$root;
				rootModel.isOperationRunning(true);
			});
			this.on("uploadprogress", function (file, progress) {
				var progressBar = $(file.previewElement.querySelector(".progress-bar"));
				progressBar.text(progress + '%');
				console.log(progress);
				if (progress === 100) {
					progressBar.removeClass('bg-dark').addClass('bg-success');
				}
			});
			this.on("queuecomplete", function() {
				var rootModel = ko.contextFor($('#filesUploadForm')[0]).$root;
				rootModel.isOperationRunning(false);
				rootModel.loadFiles();
			});
			this.on("error", function(file, errorMessage, request) {
				console.log("file upload error: status code=" + (request ? request.status : '') + ", message=" + errorMessage);
				var errorText = errorMessage;
				if (request) {
					switch (errorMessage) {
					case "InvalidName":
						errorText = 'Имя файла содержит недопустимые символы.';
						break;
					case "InvalidType":
						errorText = 'Загрузка файлов данного типа запрещена.';
						break;
					case "AlreadyExists":
						errorText = 'Файл с таким именем уже существует в данной папке.';
						break;
					case "TooBig":
						errorText = 'Разрешена загрузка файлов размером не более ' + maxFilesize + ' МБ.';
						break;
					default:
						errorText = 'Возникла неизвестная ошибка';
					}
				}
				$('[data-dz-errormessage]', file.previewElement).text(errorText);
			});
		},
	});

	$('#uploadFileModal .upload-button').click(function() {
		$('#filesUploadForm').trigger('click');
	});

	$.contextMenu({
		selector: ".file-manager .files-list .file-item",
		items: {
			copyLink: {
				name: "Скопировать ссылку", icon: 'fas fa-link', callback: function (key, opt) {
					var context = ko.contextFor(opt.$trigger[0]);
					var link = context.$root.getFileAbsoluteUrl(context.$data.relativeUrl());
					window.prompt("Ссылка на файл", link);
				}
			},
			download: {
				name: "Скачать", icon: 'fas fa-download', callback: function (key, opt) {
					var context = ko.contextFor(opt.$trigger[0]);
					var link = context.$root.getFileAbsoluteUrl(context.$data.relativeUrl());
					window.open(link, "_blank");
				}
			},
			rename: {
				name: "Переименовать", icon: 'fas fa-edit', callback: function (key, opt) {
					var context = ko.contextFor(opt.$trigger[0]);
					context.$root.showFileRenamePopup(context.$data.relativeUrl());
				}
			},
			delete: {
				name: "Удалить", icon: 'fas fa-trash', callback: function (key, opt) {
					var context = ko.contextFor(opt.$trigger[0]);
					context.$root.showFileDeletePopup(context.$data.relativeUrl());
				}
			}
		},
		events: {
			show: function (opt) {
				this.addClass('bg-light');
			},
			hide: function (opt) {
				this.removeClass('bg-light');
			}
		}
	});

	$.contextMenu({
		selector: ".file-manager",
		items: {
			empty: {

			}
		}
	});

	$('.file-manager').contextMenu(false);

	$.contextMenu({
		selector: ".file-manager .directories-list .directory-item:not(.root) .directory-title",
		items: {
			rename: {
				name: "Переименовать", icon: 'fas fa-edit', callback: function(key, opt) {
					var context = ko.contextFor(opt.$trigger[0]);
					context.$root.showDirectoryRenamePopup(context.$data);
				}
			},
			delete: {
				name: "Удалить", icon: 'fas fa-trash', callback: function(key, opt) {
					var context = ko.contextFor(opt.$trigger[0]);
					context.$root.showDirectoryDeletePopup(context.$data);
				}
			}
		},
		events: {
			show: function(opt) {
				this.addClass('bg-light');
			},
			hide: function(opt) {
				this.removeClass('bg-light');
			}
		}
	});
});

