<template>
         <button @click="logout" class="w-full p-2 text-white bg-blue-500 rounded-lg hover:bg-blue-600">Logout</button>
</template>

<script>
// Composition API
import { useAuth0 } from '@auth0/auth0-vue';

export default {
    setup() {
      const auth0 = useAuth0();

      return {
        logout() {
          auth0.logout({ 
            logoutParams: { 
              returnTo: window.location.origin 
            } 
          });
        }
      };
    }
  };

</script>

<style scoped>
/* Additional styling can go here */
</style>
