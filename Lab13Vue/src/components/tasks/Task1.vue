<script setup>
import {BASE_API_URL} from "@/assets/constants/index.js";
import {reactive, ref} from "vue";
import {useRouter} from "vue-router";
const data = reactive({
  InputData: ''
});
const output = ref("");
const router = useRouter();
async function calculate(){
  let response = await fetch(BASE_API_URL + "/tasks/task1", {
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
          <h3 class="text-center mb-4">Завдання 1</h3>
          <p class="text-center">
            У міському зоопарку містяться тварини різних видів. Для участі у міжнародній виставці «Три тварюки» зоопарк має представити трьох тварин різних видів.
            Потрібно написати програму, яка обчислить кількість способів вибрати трьох тварин для участі у виставці.
            Наприклад, якщо у зоопарку два ведмеді, тигр, лев та пінгвін, тобто сім способів обрати трьох тварин:
            перший ведмідь, тигр та лев;
            перший ведмідь, тигр та пінгвін;
            перший ведмідь, лев та пінгвін;
            другий ведмідь, тигр та лев;
            другий ведмідь, тигр та пінгвін;
            другий ведмідь, лев та пінгвін;
            тигр, лев та пінгвін.
            Вхідні дані
            Вхідний текстовий файл INPUT.TXT містить у першому рядку натуральне число n – кількість видів тварин у міському зоопарку (1 ≤ n ≤ 1000). У другому рядку через пропуск записані n натуральних чисел – кількість тварин відповідного виду. Число тварин кожного виду не перевищує 1000.
            Вихідні дані
            Вихідний текстовий файл OUTPUT.TXT має містити одне число – кількість способів вибрати трьох тварин міжнародної виставки.
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