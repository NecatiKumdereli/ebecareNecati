const vm = Vue.createApp({
    data() {
        return {
            role: {
                "name": "",
            },

        }
    },
    async mounted() {
    },
    methods: {
        async addRole() {
            let url = "/Role/Create";
            let data = vm.role;
            let result = {};
            try {
                result = await SendPostRequest(url, data);
                console.log(result);
                if (result.isSuccess == true) {
                    Swal.fire({
                        title: result.message,
                        text: "Rol sayfasýna yönlendiriliyorsunuz.",
                        icon: "success",
                        showCancelButton: false,
                        showConfirmButton: true,
                        timer: 2000,
                        willClose: () => {
                            window.location.replace("/Role/Index");
                        }
                    });
                }
                console.log(result);
            }
            catch (err) {
                console.log(err);
            }



        }
    },
}).mount("#app");