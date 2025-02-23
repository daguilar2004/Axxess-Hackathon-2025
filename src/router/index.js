import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import LoginPage from '../components/LoginPage.vue';
import SignUpPage from '../components/SignUpPage.vue';
import LearnMorePage from '../components/LearnMorePage.vue';

const routes = [

  { path: '/', component: HomePage },
  { path: '/login', component: LoginPage },
  { path: '/signup', component: SignUpPage },
  { path: '/learn-more', component: LearnMorePage }

  
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;