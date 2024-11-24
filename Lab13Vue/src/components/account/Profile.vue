<script>
  import {BASE_API_URL} from "@/assets/constants/index.js";
  import {useRouter} from "vue-router";
  import {reactive} from "vue";
  export default {
    data(){
      const data = reactive({
        username: '',
        email: '',
        fullName: '',
        phone: ''
      });

      return {
        data
      }
    },
    async created(){
      let response = await fetch(BASE_API_URL + "/user/profile", {
        method: 'GET',
        headers: {
          Authorization: 'Bearer ' + localStorage.getItem("token")
        }
      });
      if(response.ok){
        let json = await response.text();
        let jsonObj = JSON.parse(json);
        this.data.username = jsonObj["username"];
        this.data.email = jsonObj["email"];
        this.data.phone = jsonObj["phone"];
        this.data.fullName = jsonObj["fullName"];
      }
      else if(response.status === 401){
        const router = useRouter();
        await router.push("/user/login")
      }
    }
  }
</script>

<template>
  <div class="d-flex justify-content-center align-items-center min-vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 400px;">
      <h3 class="card-title text-center mb-4">Профіль користувача</h3>

      <div class="mb-3">
        <label for="Username" class="form-label">Ім'я користувача:</label>
        <p class="form-control-plaintext"><b>{{data.username}}</b></p>
      </div>

      <div class="mb-3">
        <label for="FullName" class="form-label">ФІО:</label>
        <p class="form-control-plaintext"><b>{{data.fullName}}</b></p>
      </div>

      <div class="mb-3">
        <label for="Phone" class="form-label">Телефон:</label>
        <p class="form-control-plaintext"><b>{{data.phone}}</b></p>
      </div>

      <div class="mb-3">
        <label for="Email" class="form-label">Електронна пошта:</label>
        <p class="form-control-plaintext"><b>{{data.email}}</b></p>
      </div>
    </div>
  </div>

</template>

<style scoped>

</style>