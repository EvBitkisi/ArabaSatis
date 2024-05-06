const container = document.getElementById('container');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

@if (TempData["LoginErrorMessage"] != null) {
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        @TempData["LoginErrorMessage"]
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
}

<script>
    // Uyarý mesajýný 2 saniye sonra otomatik olarak kapat
    setTimeout(function () {
        $(".alert").alert("close");
    }, 2000);
</script>
registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});
