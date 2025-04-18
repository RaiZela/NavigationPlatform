
import React, { useState } from 'react';
import './Login.css';

//import axios from 'axios';


// const API_URL = 'https://jsonplaceholder.typicode.com'; // Mock API


// export const login = async (email, password) => {
//   try {
//     const response = await axios.post(`${API_URL}/users`, { email, password });
//     return response.data;
//   } catch (error) {
//     throw error;
//   }
// };


function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errors, setErrors] = useState({});
    const { login } = useAuth();
    const navigate = useNavigate();
  
  
    const validateForm = () => {
      const newErrors = {};
      if (!email) newErrors.email = 'Email is required';
      else if (!/S+@S+.S+/.test(email)) newErrors.email = 'Email is invalid';
      if (!password) newErrors.password = 'Password is required';
      else if (password.length < 6) newErrors.password = 'Password must be at least 6 characters';
      return newErrors;
    };
  
  
    const handleSubmit = async (event) => {
      event.preventDefault();
      const formErrors = validateForm();
      if (Object.keys(formErrors).length > 0) {
        setErrors(formErrors);
      } else {
        setErrors({});
        try {
          const userData = await login(email, password);
  
  
          navigate('/profile');
          console.log('Login successful:', userData);
          // Here you would typically store the user data and redirect
        } catch (error) {
          setErrors({ form: 'Login failed. Please try again.' });
        }
      }
    };
}


export default Login;