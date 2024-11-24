import '@/assets/lib/bootstrap/dist/css/bootstrap.min.css';

import { createApp } from 'vue'
import App from './App.vue'
import { createMemoryHistory, createRouter } from 'vue-router'
import Tasks from "@/components/tasks/Tasks.vue";
import Task1 from "@/components/tasks/Task1.vue";
import Task2 from "@/components/tasks/Task2.vue";
import Task3 from "@/components/tasks/Task3.vue";
import Home from "@/components/Home.vue";
import Login from "@/components/account/Login.vue";
import Register from "@/components/account/Register.vue";
import Profile from "@/components/account/Profile.vue";
const app = createApp(App)

const routes = [
    { path: '/', component: Home },
    { path: '/tasks', component: Tasks },
    { path: '/tasks/task1', component: Task1 },
    { path: '/tasks/task2', component: Task2 },
    { path: '/tasks/task3', component: Task3 },
    { path: '/user/login', component: Login },
    { path: '/user/register', component: Register },
    { path: '/user/profile', component: Profile },
]

const router = createRouter({
    history: createMemoryHistory(),
    routes
})

app.use(router).mount('#app')

