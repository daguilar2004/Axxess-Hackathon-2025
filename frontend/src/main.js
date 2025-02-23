import { createApp } from 'vue'
import App from './App.vue'
import  './index.css'
import router from './router/index.js';  // Ensure you are importing the router


// import { createAuth0 } from '@auth0/auth0-vue';
const app = createApp(App);

// app.use(
//   createAuth0({
//     domain: "dev-qug84gqbi343u542.us.auth0.com",
//     clientId: "nEl4l6ANYdXN5yWrlz51S6tsv4UWwkZk",
//     authorizationParams: {
//       redirect_uri: window.location.origin,
//     }
//   })
// );
app.use(router);
app.mount('#app');
