<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
      <h1 class="text-2xl font-bold mb-6 text-center">Sign Up</h1>

      <!-- Name Field -->
      <div class="mb-4">
        <label for="name" class="block text-sm font-medium text-gray-700">Name:</label>
        <input
          v-model="name"
          type="text"
          id="name"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your name"
        />
      </div>

      <!-- Email Field -->
      <div class="mb-4">
        <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
        <input
          v-model="email"
          type="email"
          id="email"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your email"
        />
      </div>

      <!-- Doctor's Email Field -->
      <div class="mb-4">
        <label for="doctorEmail" class="block text-sm font-medium text-gray-700">Doctor's Email:</label>
        <input
          v-model="doctorEmail"
          type="email"
          id="doctorEmail"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your doctor's email"
        />
      </div>

      <!-- Password Field -->
      <div class="mb-6">
        <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
        <input
          v-model="password"
          type="password"
          id="password"
          class="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
          placeholder="Enter your password"
        />
      </div>

      <!-- Submit Button -->
      <button
        @click="signUp"
        class="w-full p-2 text-white bg-red-500 rounded-lg hover:bg-red-600 mb-2"
      >
        Submit
      </button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      name: '',
      email: '',
      doctorEmail: '',
      password: '',
    };
  },
  methods: {
    async signUp() {
      // First, find the doctor by email to get the DoctorId
      try {
        // const doctorResponse = await axios.get(
        //   `/doctors?email=${this.doctorEmail}`
        // );
        // console.log(doctorResponse.data)

        // if (doctorResponse.data.length === 0) {
        //   alert('Doctor not found!');
        //   return;
        // }

        // const doctorId = doctorResponse.data[0].Id;

        const newUser = {
          Name: this.name,
          Email: this.email,
          DoctorId: 111, // Pass the doctorId to the backend
        };

        const response = await axios.post('/patients', newUser);
        console.log('User created:', response.data);
        this.$router.push('/chatbox'); // Navigate to the Chatbox after successful signup
      } catch (error) {
        console.error('Error creating user:', error);
      }
    }
  }
};
</script>