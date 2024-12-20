let ImageForm = document.getElementById("ImageForm");
let ImagePriv = document.getElementById("ImagePriv");

var openFile = function (file) {
	var input = file.target;
	var reader = new FileReader();
	reader.onload = function () {
		var dataURL = reader.result;
		var output = document.getElementById('ImagePriv');
		output.src = dataURL;
		output.style.display = 'block';
		console.log(dataURL)
		console.log(input.value)
		
	};
	reader.readAsDataURL(input.files[0]);
};