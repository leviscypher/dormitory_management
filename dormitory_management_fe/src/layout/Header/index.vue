<script setup lang="ts"></script>
<template>
  <div class="position-relative iq-banner border-bottom">
    <!--Nav Start-->
    <nav class="nav navbar navbar-expand-lg navbar-light iq-navbar">
      <div class="container-fluid navbar-inner">
     
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="mb-2 navbar-nav ms-auto align-items-center navbar-list mb-lg-0">         
            <li class="nav-item dropdown">
              <a
                class="py-0 nav-link d-flex align-items-center"
                href="#"
                id="navbarDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <!-- <img
                  src="@/assets/images/avatars/01.png"
                  alt="User-Profile"
                  class="theme-color-default-img img-fluid avatar avatar-50 avatar-rounded"
                /> -->
                <div class="caption ms-3 d-none d-md-block">
                  <h6 class="mb-0 caption-title">admin</h6>
                </div>
              </a>
              <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
               <li><a class="dropdown-item" href="@/dashboard/auth/sign-in.html">Logout</a></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    
  </div>
</template>
<style lang="scss"></style>
