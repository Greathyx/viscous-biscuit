<template>
  <!--    <v-content class="my-content">-->
  <!--        <iframe src="https://i.simmer.io/@yuxuan/viscous-biscuit" frameborder="0"-->
  <!--                style="width:960px; height:600px; margin-top: 60px">-->
  <!--        </iframe>-->
  <!--    </v-content>-->
  <v-container>
    <unity
            v-bind="{hideFooter: true}"
            src="/Build/WebGL.json"
            width="1120"
            height="600"
            unityLoader="/Build/UnityLoader.js"
            style="margin-top: 40px"
    >
    </unity>
  </v-container>
</template>

<script>
  import Unity from 'vue-unity-webgl'

  export default {
    name: "Game",
    components: {
      Unity
    },
    created() {
      this.$vuetify.theme.dark = true
    },
  }
</script>

<style scoped>
  .my-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
  }
</style>
