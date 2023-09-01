const app = Vue.createApp({
    data() {
        return {
            role: {
                name: "",
            },

        }
    },
    async mounted() {
        try {
            const url = `/Role/GetById?id=` + id;
            let data = { id: id };
            let result = await SendGetRequest(url, data);
            if (result.isSuccess) {
                app.role = result.data;
                //app.modules = result.data.items;
            }
            console.log(result);
        }
        catch (err) {
            console.log(err);
        }
    },
    methods: {
        async UpdateForm() {
            try {
                const url = '/Role/Edit';
                let data = {};

                //let result = await axios.get(url, data);
                data = app.role;
                startLoader();
                let result = await SendPostRequest(url, data);
                closeLoader();
                if (result.isSuccess) {
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
            }
            catch (err) {
                console.log(err);
            }

        }
    },


}).mount("#app")