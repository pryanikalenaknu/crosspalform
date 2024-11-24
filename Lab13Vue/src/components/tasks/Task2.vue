<script setup>
import {reactive, ref} from "vue";
import {BASE_API_URL} from "@/assets/constants/index.js";
import {useRouter} from "vue-router";

const data = reactive({
  InputData: ''
});
const output = ref("");
const router = useRouter();
async function calculate(){
  let response = await fetch(BASE_API_URL + "/tasks/task2", {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
      Authorization: 'Bearer ' + localStorage.getItem("token"),
      "Content-Type": "application/json"
    }
  });
  if(response.ok){
    let json = await response.text();
    output.value = JSON.parse(json)["output"];
  }
  else if(response.status === 401){
    await router.push("/user/login");
  }
  else{

  }
}
</script>

<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="p-4 border rounded bg-light shadow">
          <h3 class="text-center mb-4">Завдання 2</h3>
          <p class="text-center">
            Дана числова послідовність, потрібно знайти довжину найбільшої підпослідовності, що зростає.
            У першому рядку вхідного файлу INPUT.TXT записано число N - довжина послідовності (1 ≤ N ≤ 1000). У другому рядку записана сама послідовність (через пропуск). Числа послідовності - цілі числа, що не перевищують 10000 по модулю.
            У вихідний файл OUTPUT.TXT потрібно вивести найбільшу довжину підпослідовності, що зростає.
          </p>

          <form @submit.prevent="calculate">
            <div class="form-group">
              <label for="InputData" class="form-label">Input Data</label>
              <textarea v-model="data.InputData" id="InputData" name="InputData" class="form-control" rows="6" placeholder="Enter your data here"></textarea>
            </div>

            <div class="form-group mt-4">
              <label for="OutputData" class="form-label">Output Data</label>
              <textarea id="OutputData" name="OutputData" class="form-control" rows="6" readonly>{{output}}</textarea>
            </div>

            <div class="text-center mt-4">
              <button type="submit" class="btn btn-primary w-100">Виконати</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>