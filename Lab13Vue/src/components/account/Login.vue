<script setup>
import {reactive, ref} from "vue";
import {BASE_API_URL} from "@/assets/constants/index.js";
import {useRouter} from "vue-router";

const data = reactive({
  email: '',
  password: ''
});
const router = useRouter();
const message = ref();
async function submit(){
  let response = await fetch(BASE_API_URL + "/user/login", {
    method: 'POST',
    body: JSON.stringify(data),
    headers:{
      "Content-Type": "application/json"
    }
  });

  if(response.ok){
    let json = await response.text();
    let token = JSON.parse(json)["token"];
    localStorage.setItem("token", token);
    await router.push("/");
    window.location.reload();
  }
  else{
    message.value = "Невірний логін або пароль."
  }

}

</script>

<template>
  <div class="d-flex justify-content-center align-items-center min-vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 400px;">
      <h3 class="card-title text-center mb-4">Вхід до акаунту</h3>
      <form class="needs-validation" novalidate @submit.prevent="submit">
        <div class="form-group mb-3">
          <label for="Email" class="form-label">Електронна адреса: </label>
          <input v-model="data.email"
                 type="email"
                 id="Email"
          placeholder="example@domain.com"
          required />
          <span class="invalid-feedback"></span>
        </div>

        <div class="form-group mb-3">
          <label for="Password" class="form-label">Пароль: </label>
          <input v-model="data.password"
                 type="password"
                 id="Password"
          placeholder="Введіть ваш пароль"
          required />
          <span class="invalid-feedback"></span>
        </div>


        <div v-if="message" class="alert alert-danger text-center mt-3">{{message}}</div>


        <div class="d-grid mt-4">
          <button type="submit" class="btn btn-primary">Увійти</button>
        </div>
        <div class="text-center mt-3">
          <RouterLink to="/user/register" class="btn btn-link">Немає акаунту? Зареєструватися</RouterLink>
        </div>
      </form>
    </div>
  </div>

</template>

<style scoped>

</style>