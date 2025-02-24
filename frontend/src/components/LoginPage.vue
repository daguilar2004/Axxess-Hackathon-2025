<template>
         <button @click="login" class="w-full p-2 text-white bg-blue-500 rounded-lg hover:bg-blue-600">Login</button>
</template>

<script>
// Composition API
import { useAuth0 } from '@auth0/auth0-vue';

export default {
  setup() {
    const auth0 = useAuth0();

    return {
      login() {
        auth0.loginWithRedirect();
      }
    };  
  }
};

</script>

<style scoped>
/* Additional styling can go here */
</style>
