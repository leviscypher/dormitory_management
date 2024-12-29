<script></script>
<template>
  <div class="auth">
    <div class="auth__header"></div>
    <div class="auth__title">
      <div class="auth_title_logo">
        <img src="@/assets/images/logo/snapedit_1726908566510.png" class="logo" alt="" />

        <div class="auth__titles">
          Trang thông tin sinh viên đăng ký túc xá trường Đại Học Giao Thông Vận Tải
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
                  <div
                    class="card card-transparent shadow-none d-flex justify-content-center mb-0 auth-card border border-dark"
                  >
                    <div class="card-body">
                      <h2 class="mb-2 text-center">Đăng nhập</h2>
                      <form>
                        <div class="row">
                          <div class="col-lg-12">
                            <div class="form-group">
                              <label for="email" class="form-label">Mã SV/CCCD/CMND</label>
                              <input
                                type="text"
                                class="form-control"
                                id="email"
                                aria-describedby="email"
                                placeholder=" "
                              />
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
                                placeholder=" "
                              />
                            </div>
                          </div>
                          <div class="col-lg-12 d-flex justify-content-between">
                            <div class="form-check mb-3">
                              <input type="checkbox" class="form-check-input" id="customCheck1" />
                              <label class="form-check-label" for="customCheck1"
                                >Nhớ mật khẩu</label
                              >
                            </div>
                            <a href="recoverpw.html">Quên mật khẩu?</a>
                          </div>
                        </div>
                        <div class="d-flex justify-content-center">
                          <button type="submit" class="btn btn-primary">Đăng nhập</button>
                        </div>
                        <router-link to="/home">
                          <p class="text-center my-3">Đăng ký ở ký túc xá?</p>
                        </router-link>
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
