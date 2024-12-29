<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '@/stores/auth'
import { useRouter } from 'vue-router'

const router = useRouter()
const authStore = useAuthStore()
const username = ref('')
const password = ref('')
const usernameError = ref('')
const passwordError = ref('')

const login = async () => {
  usernameError.value = ''
  passwordError.value = ''

  if (!username.value) {
    usernameError.value = 'Tên đăng nhập không được để trống'
  }
  if (!password.value) {
    passwordError.value = 'Mật khẩu không được để trống'
  }

  if (usernameError.value || passwordError.value) {
    return
  }

  try {
    await authStore.login(username.value, password.value)
    if (authStore.isLoggedIn) {
      alert('Đăng nhập thành công')
      router.push({ path: '/home' })
    } else {
      alert(authStore.error)
    }
  } catch (error) {
    console.error('Login error:', error)
    alert('Đã có lỗi xảy ra trong quá trình đăng nhập.')
  }
}
</script>

<template>
  <div class="auth">
    <div class="auth__header"></div>
    <div class="auth__title">
      <div class="auth_title_logo">
        <img src="@/assets/images/logo/snapedit_1726908566510.png" class="logo" alt="" />
        <div class="auth__titles">
          Trang quản trị ký túc xá trường Đại Học Giao Thông Vận Tải
        </div>
      </div>
    </div>
    <div class="auth_main">
      <div class="wrapper">
        <section class="login-content">
          <div class="row m-0 align-items-center bg-white">
            <div class="col-md-12">
              <div class="row justify-content-center">
                <div class="col-md-6">
                  <div class="card card-transparent shadow-none d-flex justify-content-center mb-0 auth-card border border-dark">
                    <div class="card-body">
                      <h2 class="mb-2 text-center">Đăng nhập</h2>
                      <form>
                        <div class="row">
                          <div class="col-lg-12">
                            <div class="form-group">
                              <label for="email" class="form-label">Tên đăng nhập</label>
                              <input
                                type="text"
                                class="form-control"
                                id="email"
                                aria-describedby="email"
                                v-model="username"
                              />
                              <p v-if="usernameError" class="text-danger">{{ usernameError }}</p>
                            </div>
                          </div>
                          <div class="col-lg-12">
                            <div class="form-group">
                              <label for="password" class="form-label">Mật khẩu</label>
                              <input
                                type="password"
                                class="form-control"
                                id="password"
                                aria-describedby="password"
                                v-model="password"
                              />
                              <p v-if="passwordError" class="text-danger">{{ passwordError }}</p>
                            </div>
                          </div>
                          <div class="col-lg-12 d-flex justify-content-between">
                            <div class="form-check mb-3">
                              <input type="checkbox" class="form-check-input" id="customCheck1" />
                              <label class="form-check-label" for="customCheck1">Nhớ mật khẩu</label>
                            </div>
                            <router-link to="/">Quên mật khẩu?</router-link>
                          </div>
                        </div>
                        <div class="d-flex justify-content-center">
                          <button type="button" @click="login" class="btn btn-primary">Đăng nhập</button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
  </div>
</template>


<style lang="css" scoped>
@import url(@/assets/css/auth/auth.css);
</style>
