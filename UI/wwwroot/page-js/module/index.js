const app = Vue.createApp({
    data() {
        return {
            pagedModules: {},
            list: [],

        }
    },
    async mounted() {
        try {
            const url = `/Module/GetList?id=${id}&page=${page}`;
            let data = {}
            let result = await SendGetRequest(url, data);
            console.log(result);
            if (result.isSuccess) {
                this.pagedModules = result.data;
                this.list = result.data.items;
                //app.modules = result.data.items;
                Swal.fire("BAIÝüÐARILI", result.message, "success");
            }
        }
        catch (err) {
            console.log(err);
        }
        CloseInitLoader();
    },
    methods: {
         DeleteModule(id,name) {
            Swal.fire({
                title: "Emin Misiniz?",
                text: name + " isimli modülü silmek istediðinizden emin misiniz?",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet',
                cancelButtonText: 'Hayýr'
            }).then(async (result) => {
                if (result.isConfirmed) {
                    const url = '/Module/Delete';
                    let data = {id: id };

                    //let result = await axios.get(url, data);
                    //data = app.module;
                    startLoader();
                    let result = await SendGetRequest(url, data);
                    closeLoader();
                    if (result.isSuccess) {
                        Swal.fire({
                            title: result.message,
                            text: "Modül sayfasýna yönlendiriliyorsunuz.",
                            icon: "success",
                            showCancelButton: false,
                            showConfirmButton: true,
                            timer: 2000,
                            willClose: () => {
                                window.location.replace("/Module/Index");
                            }
                        });
                    }

                }
            });
        }
    },


}).mount("#app")