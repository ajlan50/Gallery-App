
let btnDelete = document.querySelectorAll(".btn-delete");

btnDelete.forEach(btn => {
	btn.addEventListener("click", () => {
		const swal = Swal.mixin({
			customClass: {
				confirmButton: 'btn btn-outline-danger mx-2',
				cancelButton: 'btn btn-light'
			},
			buttonsStyling: false
		});

		swal.fire({
			title: 'Are you sure that you need to delete this Photo?',
			text: "You won't be able to revert this!",
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Yes, delete it!',
			cancelButtonText: 'No, cancel!',
			reverseButtons: true
		}).then((result) => {
			if (result.isConfirmed) {

				fetch(`/Photos/Delete/${btn.getAttribute('data-Id')}`, {
					method: "DELETE"
				}).then((res) => {
					console.log(res);
					if (!res.ok) {

						swal.fire(
							'Oooops...',
							'Something went wrong.',
							'error'
						)
						//	throw new Erorr("DLETE IS NOT WORGING")
					}
					else {
						btn.parentElement.parentElement.parentElement.remove();
						swal.fire(
							'Deleted!',
							'Photo has been deleted.',
							'success'
						);
						console.log(btn.parentElement.parentElement.parentElement)

						//return  res.json();
					}

				}).then(data => {
					console.log(data)
				}).catch((er) => {
					console.error('Fetch error:', er);
				});

			}
		});
	})

})