<template>
  <v-app>
    <v-app-bar
            app
            clipped-left
            clipped-right
            dark
    >
      <v-toolbar-title style="overflow: visible; font-weight: bold; margin-right: 10px">
        Viscous Biscuit
      </v-toolbar-title>
      <v-tabs id="tab" color="white">
        <v-tabs-slider color="yellow"></v-tabs-slider>
        <v-tab>
          <router-link to="/" class="link">Home</router-link>
        </v-tab>
        <v-tab>
          <router-link to="/play_demo" class="link">Play Demo</router-link>
        </v-tab>
      </v-tabs>
    </v-app-bar>

    <router-view/>

  </v-app>
</template>

<script>
  export default {
    name: 'App',

    components: {},

    data: () => ({
      //
    }),

    created() {
      this.$vuetify.theme.dark = true
    },
  };
</script>

<style>
  #tab a {
    /*font-weight: bold;*/
    text-decoration: none;
    color: white;
  }

  #tab a.router-link-exact-active {
    text-decoration: none;
    color: white;
  }

  .link {
    height: 64px;
    align-items: center;
    display: flex;
    /*font-size: 1rem;*/
    font-size: 0.875rem;
  }
</style>
