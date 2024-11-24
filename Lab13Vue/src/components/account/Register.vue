<script setup>
  import {reactive, ref} from 'vue'
  import {BASE_API_URL} from "@/assets/constants/index.js";
  import {useRouter} from "vue-router";
  const data = reactive({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    fullName: '',
    phone: ''
  });
  const router = useRouter();
  async function submit(){
    if(data.confirmPassword !== data.password){
      return;
    }
    let response = await fetch(BASE_API_URL + "/user/register", {
      method: 'POST',
      body: JSON.stringify(data),
      headers:{
        "Content-Type": "application/json"
      }
    });
    console.log(JSON.stringify(data))
    if(response.ok){
      await router.push("/user/login");
    }
    else{

    }
  }

</script>

<template>
  <div class="d-flex justify-content-center align-items-center min-vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 450px;">
      <h3 class="card-title text-center mb-4">Реєстрація</h3>

      <form @submit.prevent="submit">
        <div class="mb-3">
          <label for="Username" class="form-label">Логін (до 50 символів)</label>
          <input v-model="data.username" type="text" id="Username" name="Username" class="form-control" placeholder="Введіть ваш логін" />
        </div>

        <div class="mb-3">
          <label for="FullName" class="form-label">Ваше повне ім’я</label>
          <input v-model="data.fullName" type="text" id="FullName" name="FullName" class="form-control" placeholder="Введіть ваше повне ім’я" />
        </div>

        <div class="mb-3">
          <label for="Password" class="form-label">Пароль</label><br>
          <small class="form-text text-muted">
            Повинен містити мінімум 8 символів, включаючи одну цифру, спеціальний знак і велику літеру.
          </small>
          <input v-model="data.password" type="password" id="Password" name="Password" class="form-control" placeholder="Введіть пароль" />
        </div>

        <div class="mb-3">
          <label for="ConfirmPassword" class="form-label">Підтвердіть пароль</label>
          <input v-model="data.confirmPassword" type="password" id="ConfirmPassword" name="ConfirmPassword" class="form-control" placeholder="Повторіть пароль" />
        </div>

        <div class="mb-3">
          <label for="PhoneNumber" class="form-label">Номер телефону</label><br>
          <small class="form-text text-muted">Введіть номер телефону у форматі України.</small>
          <input v-model="data.phone" type="text" id="PhoneNumber" name="PhoneNumber" class="form-control" placeholder="+380XXXXXXXXX" />
        </div>

        <div class="mb-3">
          <label for="Email" class="form-label">Електронна пошта</label>
          <input v-model="data.email" type="email" id="Email" name="Email" class="form-control" placeholder="example@domain.com" />
        </div>

        <div class="text-center mt-4">
          <button type="submit" class="btn btn-success w-100">Створити акаунт</button>
          <RouterLink to="/user/login" class="btn btn-link mt-2">Вже є акаунт? Увійти</RouterLink>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>