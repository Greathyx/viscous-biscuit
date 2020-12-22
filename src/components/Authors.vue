<template>
    <v-item-group mandatory>
        <v-container>
            <v-row
                    style="display: flex; flex-direction: row; justify-content: center;"
                    justify="space-around"
            >
                <v-col
                        v-for="(author, i) in authors"
                        :key="i"
                        cols="12"
                        md="2"
                >
                    <v-item>
                        <div style="display: flex; flex-direction: column; align-items: center;">
                            <v-avatar size="160">
                                <img
                                        :src="author.src"
                                        :alt="author.name"
                                >
                            </v-avatar>
                            <div style="margin-top: 20px">
                                <div class="title text--primary" v-html="author.name" style="text-align: center"></div>
                                <div class="text--primary" v-html="author.position" style="text-align: center"></div>
                                <div class="text--primary" v-html="author.position2 ? author.position2:null" style="text-align: center"></div>
                            </div>
                        </div>
                    </v-item>
                </v-col>
            </v-row>
        </v-container>
    </v-item-group>
</template>

<script>
    import david_pic from '../assets/authors/david.jpg';
    import konrad_pic from '../assets/authors/konrad.png';
    import matilda_pic from '../assets/authors/matilda.jpg';
    import platon_pic from '../assets/authors/platon.jpg';
    import yuxuan_pic from '../assets/authors/yuxuanh.jpg'

    export default {
        name: "Authors",
        created() {
            this.$vuetify.theme.dark = false
        },
        data: () => ({
            authors: [
                {
                    src: david_pic,
                    name: 'David Jacobsson',
                    position: 'Team Manager'
                },
                {
                    src: konrad_pic,
                    name: 'Konrad Wihl',
                    position: 'Creative Director'
                },
                {
                    src: matilda_pic,
                    name: 'Matilda Richardsson',
                    position: 'Visual Artist',
                    position2: 'Level Designer',
                },
                {
                    src: platon_pic,
                    name: 'Platon Woxler',
                    position: 'Programmer'
                },
                {
                    src: yuxuan_pic,
                    name: 'Yuxuan Huang',
                    position: 'Programmer',
                    position2: 'Website Designer',
                }
            ]
        }),
    }
</script>
