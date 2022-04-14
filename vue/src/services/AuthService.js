import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/login/register', user)
  },

  registerNewMember(user){
    return axios.post('/login/register/newMember', user)
  }

}