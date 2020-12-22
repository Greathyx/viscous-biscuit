<template>
  <v-app>
    <v-parallax src="../assets/conceptart_parallax.jpg" style="height: 100vh">
      <v-row
              align="center"
              justify="center"
      >
        <v-col class="text-center" cols="12">
          <h1 class="font-weight-bold display-4">Viscous Biscuit</h1>
          <h4 class="font-weight-medium headline"
              style="margin-top: 20px">
            Solve the puzzle and get out!
          </h4>
        </v-col>
      </v-row>
    </v-parallax>
    <!--        <img alt="Vue logo" src="./assets/conceptart.png">-->

    <div class="my-container">

      <div class="video">
        <h3 class="display-1 font-weight-medium" style="color: white">GAMEPLAY TRAILER</h3>
        <iframe class="my-iframe"
                src="https://www.youtube.com/embed/zcL_iGtOqPc" frameborder="0"
                allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
                style="margin-top: 20px"
                allowfullscreen
        ></iframe>
      </div>

      <v-row class="about1">
        <v-col sm="12" md="6" class="col_left1">
          <v-img src="../assets/blob.jpg" alt="blob" style="width: 100%; height: 100%"></v-img>
        </v-col>

        <v-col sm="12" md="6" class="col_right1">
          <h1 style="margin-bottom: 10px; padding: 0 50px">STORY & CHARACTER</h1>
          <p style="padding: 0 50px; text-align: justify">
            Biscuit is an experimental cookie dough concocted in a secret bakery laboratory.
            He wakes up, having fallen from the conveyor belt into the oven - this is an opportunity to
            escape! Exploring his way upwards through the maze of strange lab facilities, he gradually
            discovers the ability to change his own physical state (solid, gas & liquid) and thus
            overcome increasingly challenging obstacles.
          </p>
        </v-col>

        <v-col sm="12" class="col_down">
          <h1>GAME OVERVIEW</h1>
          <p style="padding: 10px 15% 0 15%; text-align: justify">
            Biscuit starts the game in the spawning room, which serves as the central hub between
            rooms branching off to the sides. Upon leaving the spawn room to the left and to the right,
            Biscuit enters two single-entrance rooms with puzzles in each of them.
            In the far side of each room is a generator connected to the cable from the fuse box.
            The goal is to reach them and smash them in order to turn off the big fan. <br>
            When breaking either of the generators and passing back through the spawn room to the other one,
            Biscuit will notice one of the cables/lightbulbs on the fuse box being dead,
            but the fan still animated. Once breaking both generators,
            Biscuit may ascend into the shaft above the fan to continue their journey.<br>
          </p>
          <v-carousel
                  cycle
                  style="width: 70%"
                  hide-delimiter-background
                  show-arrows-on-hover
          >
            <v-carousel-item
                    v-for="(room,i) in game_rooms"
                    :key="i"
                    :src="room.src"
                    reverse-transition="fade-transition"
            ></v-carousel-item>
          </v-carousel>
<!--          <img src="../assets/IMG_2088.jpeg" alt="" style="width: 80%">-->
        </v-col>

        <v-col sm="12" style="padding: 0; margin: 60px 0;">
          <h1 style="margin-bottom: 10px; padding: 0 50px; text-align: center">TEAM</h1>
          <Authors/>
        </v-col>

        <v-col sm="12" style="padding: 0">
          <v-footer
                  default
                  padless
                  class="font-weight-medium"
                  dark
          >
            <v-col
                    class="text-center"
                    cols="12"
            >
              {{ new Date().getFullYear() }} — <strong>Viscous Biscuit</strong>
            </v-col>
          </v-footer>
        </v-col>
      </v-row>
    </div>

  </v-app>
</template>

<script>
  import Authors from '../components/Authors'
  import gas from '../assets/gas.png'
  import liquid from '../assets/liquid.png'
  import spawning from '../assets/spawning.png'

  export default {
    name: 'Home',
    components: {
      Authors
    },
    data () {
      return {
        game_rooms: [
          {
            src: spawning,
          },
          {
            src: liquid,
          },
          {
            src: gas,
          }
        ],
      }
    },
  }
</script>

<style>
  .my-container {
    width: 100%;
    padding: 0;
    margin: 0;
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .video {
    padding: 0 6%;
    width: 100%;
    height: 93vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background-size: cover;
    background-position: center center;
    background-image: url("../assets/video_background.gif");
  }

  .my-iframe {
    height: calc(75vh);
    width: 100%;
  }

  .about1 {
    width: 100%;
    height: 60vh;
    /*background-color: antiquewhite;*/
  }

  .col_left1 {
    height: inherit;
    padding: 0;
    /*display: flex;*/
    /*flex-direction: column;*/
    /*justify-content: center;*/
    /*align-items: flex-end;*/
    /*background-size: cover;*/
    /*background-position: center center;*/
    /*background-image: url("../assets/blob.jpg");*/
  }

  .col_right1 {
    background-color: antiquewhite;
    height: inherit;
    color: black;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: flex-start;
  }

  .col_down {
    width: 100%;
    height: 93vh;
    color: black;
    background-color: lightblue;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
</style>
