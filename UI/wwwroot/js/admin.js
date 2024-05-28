$(document).ready(function () {
	$.datetimepicker.setLocale('ru');
	$('.dateTimePicker').datetimepicker({ format: 'd.m.Y H:i:s', step: 5, dayOfWeekStart: 1 });
	$('.datePicker').datetimepicker({ format: 'd.m.Y', timepicker: false, dayOfWeekStart: 1 });
	$('.timePicker').datetimepicker({ format: 'H:i', datepicker: false, step: 5 });
	$('form .dateTimePicker, form .datePicker').each(function (index, element) {
		if ($.data(element.form, 'validator')) {
			$(element).rules("remove");
		}
	});

	$('body').on('click', '.delete-object-button', function (clickEvent) {
		var result = confirm('Вы уверены, что хотите безвозвратно удалить объект?');
		if (!result) {
			clickEvent.stopImmediatePropagation();
			clickEvent.preventDefault();
		}
	});

	tinyMCE.init({
		language: "ru",
		selector: ".tinymce",
		plugins: [
			"advlist autolink lists link image charmap hr anchor pagebreak",
			"searchreplace wordcount visualblocks visualchars",
			"insertdatetime media nonbreaking save table directionality",
			"template paste code spellchecker"
		],
		toolbar1: "insertfile undo redo | styleselect fontsizeselect | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | spellchecker | forecolor backcolor",
		fontsize_formats: "8px 10px 12px 14px 18px 24px 36px",
		convert_urls: false,
		allow_script_urls: true,
		extended_valid_elements: "a[*]",
		spellchecker_languages: "Russian=ru,English=en",
		spellchecker_language: "ru",
		spellchecker_rpc_url: "http://speller.yandex.net/services/tinyspell?options=12",
		branding: false,
		file_picker_callback: function (callback, value, meta) {
			console.log(this);
			this.windowManager.openUrl({
				title: 'Выбор файла',
				url: pageUtils.resolvePath('/Admin/FileManager?environment=TinyMce'),
				buttons: [
					{
						type: 'cancel',
						name: 'cancel',
						text: 'Отмена'
					},
					{
						type: 'custom',
						name: 'returnFile',
						text: 'Выбрать',
						primary: true,
					}
				],
				onAction: function (instance, details) {
					if (details.name === 'returnFile') {
						instance.sendMessage('returnFile');
					}
				},
				onMessage: function (instance, details) {
					console.log(details);
					if (details.mceAction === 'processFile') {
						var fileUrl = pageUtils.resolvePath(details.content.url);
						if (meta.filetype === 'file') {
							callback(fileUrl, { text: details.content.name });
						} else {
							callback(fileUrl);
						}
						instance.close();
					}
				}
			});
		}
	});
});