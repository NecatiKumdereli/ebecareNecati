const app = Vue.createApp({
    data() {
        return {
            roles: {},
            message: "",

        }
    },
    async mounted() {
        try {
            const url = '/Role/GetList';
            let data = {}
            let result = await SendGetRequest(url, data);
            if (result.isSuccess) {
                app.roles = result.data;
                //app.modules = result.data.items;
            }
            console.log(result);
        }
        catch (err) {
            console.log(err);
        }
        CloseInitLoader();
    },
    methods: {
        DeleteRole(id, name) {
            Swal.fire({
                title: "Emin Misiniz?",
                text: name + ' isimli modülü silmek istediðinizden emin misiniz?',
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet',
                cancelButtonText: 'Hayýr',
            }).then(async (result) => {
                if (result.isConfirmed) {
                    const url = '/Role/Delete?id=' + id;
                    let data = {};

                    //let result = await axios.get(url, data);
                    startLoader();
                    let result = await SendGetRequest(url, data);
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
            });
        },
        
    },
}).mount("#app")