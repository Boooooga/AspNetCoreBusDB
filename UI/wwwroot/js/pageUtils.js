window.pageUtils = (function (window) {
	var appRootPath = '';
	var fileManagerOpener;
	var isFileManagerSetupCompleted = false;

	return {
		setAppRootPath: function (path) {
			if (!path || path.length === 0) {
				appRootPath = '/';
				return;
			}
			if (path.slice(-1) !== '/') {
				path = path + '/';
			}
			appRootPath = path;
		},

		resolvePath: function (path) {
			if (!path || path.length === 0) {
				return path;
			}
			if (path[0] === '/')
				path = path.substr(1);
			return appRootPath + path;
		},

		setupPopupFileManager: function () {
			window.addEventListener('message', function (event) {
				var data = event.data;
				if (data.type !== 'SpaceApp.FileManager.FileSelected') {
					return;
				}
				console.log(data);
				var fileSelectedEvent = $.Event('spaceAppFileManager:fileSelected');
				fileSelectedEvent.url = data.content.url;
				fileManagerOpener.trigger(fileSelectedEvent);
				fileManagerOpener = undefined;
				$('#fileManagerModal').modal('hide');
			});
			isFileManagerSetupCompleted = true;
		},

		openPopupFileManager: function (element) {
			if (!isFileManagerSetupCompleted) {
				return;
			}
			fileManagerOpener = $(element);
			$('#fileManagerModal').modal('show');
		},

		getSelectedFile: function () {
			$('#fileManagerModal iframe')[0].contentWindow.postMessage('returnFile', document.location.origin);
		},

		getDefaultFileSelectedHandler: function() {
			return function (event) {
				var mediaBlock = $(this).parents('.media');
				var inputField = $($('input[type="text"]', mediaBlock)[0]);
				inputField.val(event.url);
				$('.media-thumbnail', mediaBlock).css('background-image', "url('" + pageUtils.resolvePath(event.url) + "')");
			};
		},

		bindDefaultFileManagerHandlers: function (selector) {
			$('body').on('click', selector, function() {
				pageUtils.openPopupFileManager(this);
			});
			$('body').on('spaceAppFileManager:fileSelected', selector, pageUtils.getDefaultFileSelectedHandler());
		}
	};
})(window);