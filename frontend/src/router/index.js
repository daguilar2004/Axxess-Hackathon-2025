import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import LoginPage from '../components/LoginPage.vue'; 
import LogoutPage from '../components/LogoutPage.vue'; 
import SignUpPage from '../components/SignupPage.vue';
import LearnMorePage from '../components/LearnMorePage.vue';
import ChatBox from '../views/ChatBox.vue';
//import ChatBox from '../views/ChatTest.vue';

const routes = [

  { path: '/', component: HomePage },
  { path: '/login', component: LoginPage },
  { path: '/logout', component: LogoutPage },
  { path: '/signup', component: SignUpPage },
  { path: '/learn-more', component: LearnMorePage },
  { path: '/chatBox', component: ChatBox }
  
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;