<template>
    <div class="flex items-center justify-center min-h-screen bg-gray-100">
      <div class="w-full max-w-md p-6 bg-white rounded-2xl shadow-lg">
        <h1 class="text-3xl font-bold text-center text-gray-800 mb-6">Welcome to Our App!</h1>
        
        <div class="mb-4 text-center">
          <p class="text-lg text-gray-700 mb-4">What would you like to do?</p>
  
          <!-- Navigation buttons -->
          <button @click="goToSignUp" class="w-full p-2 text-white bg-green-500 rounded-lg hover:bg-green-600 mb-2">
            Sign Up
          </button>
          <button @click="goToLogin" class="w-full p-2 text-white bg-red-500 rounded-lg hover:bg-red-600 mb-2">
            Login
          </button>

        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { useRouter } from 'vue-router';
  import { useAuth0 } from "@auth0/auth0-vue";
  import { onMounted } from "vue"


  
  export default {

    name: 'HomePage',
    setup() {
      const { isAuthenticated, isLoading } = useAuth0(); // Destructure correctly to get `isAuthenticated`
      const router = useRouter(); // Ensure this is initialized
  
      onMounted(() => {
        if (!isLoading.value && isAuthenticated.value) {
          router.push("/chatbox");
        }
      });

      const goToSignUp = () => {
        router.push('/signup'); // Navigate to SignUp page
      };
  
      const goToLogin = () => {
        router.push('/login'); // Navigate to Login page
      };

      const goToLogout = () => {
        router.push('/logout'); // Navigate to Logout page
      };
  
      const goToLearnMore = () => {
        router.push('/learn-more'); // Navigate to Learn More page
      };
  
      return {
        goToSignUp,
        goToLogin,
        goToLearnMore,
        goToLogout
        
      };
    }
  };
  </script>
  