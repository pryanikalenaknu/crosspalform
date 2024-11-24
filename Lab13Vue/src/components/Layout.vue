<script setup>
function isLogin(){
  return localStorage.getItem('token') !== null;
}
function Logout(){
  localStorage.removeItem("token");
  window.location.reload();
}
</script>

<template>
  <html lang="en">
  <head>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Lab5</title>
  </head>
  <body>
  <header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
      <div class="container-fluid">
        <RouterLink class="navbar-brand" to="/">Lab5</RouterLink>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
          <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/">Home</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/tasks">Labs</RouterLink>
            </li>
          </ul>

        </div>
        <div style="justify-self: right">
          <ul class="navbar-nav flex-grow-1" v-if="isLogin()">
            <li class="nav-item">
              <a href="/" class="nav-link" @click.prevent="Logout()">Logout</a>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link" to="/user/profile">Profile</RouterLink>
            </li>
          </ul>
          <ul class="navbar-nav flex-grow-1" v-else>
            <li class="nav-item">
              <RouterLink class="nav-link" to="/user/login">Login</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link" to="/user/register">Register</RouterLink>
            </li>
          </ul>

        </div>
      </div>
    </nav>
  </header>

  </body>
  </html>
</template>

<style scoped>
a.navbar-brand {
  white-space: normal;
  text-align: center;
  word-break: break-all;
}

a {
  color: #0077cc;
}



.nav-pills .nav-link.active, .nav-pills .show > .nav-link {
  color: #fff;
  background-color: #1b6ec2;
  border-color: #1861ac;
}


.border-bottom {
  border-bottom: 1px solid #e5e5e5;
}

.box-shadow {
  box-shadow: 0 .25rem .75rem rgba(0, 0, 0, .05);
}


</style>