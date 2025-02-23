<template>
    <div class="flex flex-col h-screen bg-gray-100">
      <div class="relative w-1/2 h-screen bg-red-500 rounded-br-full flex justify-center items-center text-center p-5 animate-slideIn self-start ml-0">
        <div class="content">
          <h1 class="text-4xl font-bold text-white mb-0 leading-tight opacity-0 transform translate-y-5 animate-slideUpMain">Welcome to Blossom, Your AI-Powered Health Assistant</h1>
          <p class="text-lg text-white mt-5 leading-relaxed opacity-0 transform translate-y-5 animate-slideUpSub">
            Your health matters. Our intelligent chatbot listens to your symptoms and provides an AI-generated pre-diagnosis, helping you understand potential health concerns before you see a doctor.
          </p>
          <div class="flex gap-7 mt-7 justify-center opacity-0 transform translate-y-5 animate-slideUpButtons" id="auth-buttons-anchor">
            <button @click="Login" class="px-5 py-2 text-lg font-bold text-white bg-transparent border-2 border-white rounded-3xl w-32 hover:text-red-500 hover:bg-white">Login</button>
            <button class="px-5 py-2 text-lg font-bold text-white bg-transparent border-2 border-white rounded-3xl w-32 hover:text-red-500 hover:bg-white">Sign Up</button>
          </div>
        </div>
      </div>
  
      <div class="mx-auto mt-20 p-5 max-w-4xl w-9/10">
        <div class="how-it-works">
          <h2 class="text-3xl font-bold text-gray-800 mb-5 text-center">How It Works</h2>
          <div class="flex flex-col items-center">
            <div class="text-center mb-7">
              <h3 class="text-2xl text-gray-800 mb-2">Describe Your Symptoms</h3>
              <p class="text-lg text-gray-600">Enter details about your health concerns in a simple chat format.</p>
            </div>
            <div class="text-center mb-7">
              <h3 class="text-2xl text-gray-800 mb-2">Get an AI-Powered Pre-Diagnosis</h3>
              <p class="text-lg text-gray-600">Our advanced system analyzes your input and provides a possible explanation.</p>
            </div>
            <div class="text-center mb-7">
              <h3 class="text-2xl text-gray-800 mb-2">Share with a Doctor</h3>
              <p class="text-lg text-gray-600">The generated health report is shared with a medical professional, who reviews your case and suggests necessary tests or treatments.</p>
            </div>
          </div>
        </div>
      </div>
  
      <div class="mx-auto mt-20 p-5 max-w-4xl w-9/10">
        <div class="why-choose">
          <h2 class="text-3xl font-bold text-gray-800 mb-5 text-center">Why Choose Blossom?</h2>
          <ul class="list-none p-0 text-lg text-gray-600 text-center">
            <li class="mb-3">✅ Quick & Easy – No waiting time, get instant insights.</li>
            <li class="mb-3">✅ Smart & Secure – AI-powered analysis with strict data privacy.</li>
            <li class="mb-3">✅ Doctor-Assisted – Your health report is reviewed by professionals for better accuracy.</li>
          </ul>
        </div>
      </div>
  
      <div class="text-center mt-10 text-2xl text-gray-800">
        <p>Start your health journey <span class="cursor-pointer underline text-blue-600" @click="scrollToAuthButtons">today</span>! 🚀</p>
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
      const { isAuthenticated, isLoading } = useAuth0(); // Destructure correctly to get isAuthenticated
      const auth0 = useAuth0();
      const router = useRouter(); // Ensure this is initialized

      onMounted(() => {
        if (!isLoading.value && isAuthenticated.value) {
          router.push("/chatbox");
        }
      });

      const goToSignUp = () => {
        router.push('/signup'); // Navigate to SignUp page
      };

      



      return {
        goToSignUp,
        Login() {
        auth0.loginWithRedirect();
      }
      };
    },

    methods: {
      scrollToAuthButtons() {
        const element = document.getElementById('auth-buttons-anchor');
        if (element) {
          element.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
      },
    },
  };
  </script>
  
  <style scoped>
  /* Animations */
  @keyframes slideIn {
    0% {
      width: 0;
    }
    100% {
      width: 50vw;
    }
  }
  
  @keyframes slideUpMain {
    0% {
      opacity: 0;
      transform: translateY(20px);
    }
    100% {
      opacity: 1;
      transform: translateY(0);
    }
  }
  
  @keyframes slideUpSub {
    0% {
      opacity: 0;
      transform: translateY(20px);
    }
    100% {
      opacity: 1;
      transform: translateY(0);
    }
  }
  
  @keyframes slideUpButtons {
      0% {
          opacity: 0;
          transform: translateY(20px);
      }
      100% {
          opacity: 1;
          transform: translateY(0);
      }
  }
  
  .animate-slideIn {
    animation: slideIn 1s forwards;
  }
  
  .animate-slideUpMain {
    animation: slideUpMain 0.6s 0.5s ease-out forwards;
  }
  
  .animate-slideUpSub {
    animation: slideUpSub 0.6s 1s ease-out forwards;
  }
  
  .animate-slideUpButtons {
      animation: slideUpButtons 0.6s 1.5s ease-out forwards;
  }
  
  .bordered-container {
    border: transparent;
    border-radius: 30px;
    box-shadow: 0 4px 8px rgba(226, 89, 89, 0.2);
  }
  </style>