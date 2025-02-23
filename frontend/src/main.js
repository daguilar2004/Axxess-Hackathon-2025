import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index.js';  // Ensure you are importing the router
import  './index.css'
import axios from 'axios'



import { createAuth0 } from '@auth0/auth0-vue';
const app = createApp(App);

const DOTNET_API = "http://localhost:5243"
axios.defaults.baseURL = DOTNET_API;


app.use(
  createAuth0({
    domain: "dev-qug84gqbi343u542.us.auth0.com",
    clientId: "pfjIj0QDI640Q7M2sE2bL8hwLiY4lnNF",
    authorizationParams: {
      redirect_uri: window.location.origin + '/chatbox',
    }
  })
);
app.use(router);
app.mount('#app');
