<script setup lang="ts"></script>
<template>
    <div class="auth__title">
      <div class="auth_title_logo">
        <img src="@/assets/images/logo/snapedit_1726908566510.png" class="logo" alt="" />

        <div class="auth__titles">
          Trang thông tin sinh viên đăng ký túc xá trường Đại Học Giao Thông Vận Tải
        </div>
      </div>
    </div>
</template>
<style>
.auth {
}
.auth__header {
  height: 5px;
}
.auth_title_logo {
  display: flex;
  justify-content: center;
  align-items: center;
  background: #fff;
  padding-top: 15px;
  padding-bottom: 15px;
}
img.logo {
  width: 115px;
  height: 80px;
  object-fit: cover;
}
.auth__titles {
  font-size: 22px;
  font-family: sans-serif;
  text-transform: uppercase;
  font-weight: 600;
}
</style>
