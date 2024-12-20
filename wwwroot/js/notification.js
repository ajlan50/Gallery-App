let notifContainer = document.getElementById('notifContainer')
console.log( notifContainer)
function creatNotification(text,alertKind ,withTimer) {

    
    let div = document.createElement('div');

    div.classList = 'alert alert-success alert-dismissible fade show'
    div.setAttribute('role',"alert")
    div.innerHTML = `
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            <strong style="color:green;">Success Messeage</strong> ${text}
    `
    notifContainer.appendChild(div);


    if (withTimer == true)
    {
        setTimeout(() => {
            div.remove();
        }, 4000)
    }
}

 


