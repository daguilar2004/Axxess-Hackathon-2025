import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import LoginPage from '../components/LoginPage.vue'; 
import SignUpPage from '../components/SignupPage.vue';
import ChatBox from '../views/ChatBox.vue';
const routes = [
  { path: '/login', component: LoginPage },
  { path: '/signup', component: SignUpPage },
  { path: '/', component: HomePage },
  { path: '/chatbox', component: ChatBox, meta: { requiresAuth: true } }
  
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;