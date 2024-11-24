<script>
import {useRouter} from "vue-router";

export default {
  created() {
    let router = useRouter();
    if(localStorage.getItem("token") === null){
      router.push("/user/login");
    }
  }
}
</script>

<template>
  <div class="d-flex justify-content-center align-items-center min-vh-100 bg-light">
    <div class="card shadow-sm p-4" style="width: 500px;">
      <h3 class="card-title text-center mb-4">Виберіть завдання</h3>
      <p class="text-muted text-center">
        Будь ласка, оберіть одне із доступних завдань нижче.
      </p>

      <div class="list-group">
        <RouterLink to="/tasks/task1" class="list-group-item list-group-item-action text-center">
          Завдання 1
        </RouterLink>
        <RouterLink to="/tasks/task2" class="list-group-item list-group-item-action text-center">
          Завдання 2
        </RouterLink>
        <RouterLink to="/tasks/task3" class="list-group-item list-group-item-action text-center">
          Завдання 3
        </RouterLink>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>