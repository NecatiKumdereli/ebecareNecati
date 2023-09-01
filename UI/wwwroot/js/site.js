// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SendPostRequest(url, data) {
    return new Promise((resolve, reject) => {
        let config = {
            headers: {
                'Content-Type': 'application/json; chartset-UTF-8'
            }
        };
        axios.post(url, data, config)
            .then(response => {
                resolve(response.data);
            })
            .catch(error => {
                reject(error);
            });
    });
}

function SendGetRequest(url, params) {
    console.log("Parametler", params);
    return new Promise((resolve, reject) => {
        let config = {
            headers: {
                'Content-Type': 'application/json; chartset-UTF-8'
            }
        };
        
        axios.get(url, { params }, config)
            .then(response => {
                resolve(response.data);
            })
            .catch(error => {
                reject(error);
            });
    });
}

function startLoader() {
    Swal.fire({
        title: 'Lütfen Bekleyiniz...',
        allowOutsideClick: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading()
        }
    });
}

function closeLoader() {
    Swal.close();
}

const InitLoader = () => $('#load_screen').css('display', 'block');
const CloseInitLoader = () => $('#load_screen').css('display', 'none');