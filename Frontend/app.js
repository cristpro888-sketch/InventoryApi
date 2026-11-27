const API_URL = "http://localhost:5176/api";

// Elementos del DOM
const authSection = document.getElementById("auth-section");
const dashboardSection = document.getElementById("dashboard-section");
const authForm = document.getElementById("auth-form");
const btnRegister = document.getElementById("btn-register");
const btnLogout = document.getElementById("btn-logout");
const authMessage = document.getElementById("auth-message");

const productForm = document.getElementById("product-form");
const formTitle = document.getElementById("form-title");
const btnCancelEdit = document.getElementById("btn-cancel-edit");
const productsTableBody = document.getElementById("products-table-body");

// Verificar si ya existe un token guardado al cargar la página
document.addEventListener("DOMContentLoaded", () => {
    const token = localStorage.getItem("jwt_token");
    if (token) {
        showDashboard();
    }
});

// Iniciar Sesión
authForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();

    try {
        const response = await fetch(`${API_URL}/auth/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const data = await response.json();

        if (!response.ok) {
            showMessage(data.message || "Credenciales incorrectas.", "red");
            return;
        }

        localStorage.setItem("jwt_token", data.token);
        authForm.reset();
        showDashboard();
    } catch (error) {
        showMessage("Error de conexión con el servidor.", "red");
    }
});

// Registrar Usuario
btnRegister.addEventListener("click", async () => {
    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();

    if (username.length < 4 || password.length < 6) {
        showMessage("Usuario mín. 4 caracteres y contraseña mín. 6.", "red");
        return;
    }

    try {
        const response = await fetch(`${API_URL}/auth/register`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const data = await response.json();

        if (!response.ok) {
            showMessage(data.message || "Error al registrar usuario.", "red");
            return;
        }

        showMessage("Usuario registrado. Ahora puedes iniciar sesión.", "green");
    } catch (error) {
        showMessage("Error de conexión con el servidor.", "red");
    }
});

// Cerrar Sesión
btnLogout.addEventListener("click", () => {
    localStorage.removeItem("jwt_token");
    dashboardSection.classList.add("hidden");
    authSection.classList.remove("hidden");
    authMessage.textContent = "";
});

// Mostrar Panel y Cargar Productos
function showDashboard() {
    authSection.classList.add("hidden");
    dashboardSection.classList.remove("hidden");
    loadProducts();
}

// Obtener Productos (GET)
async function loadProducts() {
    const token = localStorage.getItem("jwt_token");

    try {
        const response = await fetch(`${API_URL}/products`, {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (response.status === 401) {
            btnLogout.click();
            return;
        }

        const products = await response.json();
        renderProducts(products);
    } catch (error) {
        console.error("Error cargando productos:", error);
    }
}

// Dibujar filas en la tabla HTML
function renderProducts(products) {
    productsTableBody.innerHTML = "";

    products.forEach((p) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${p.id}</td>
            <td>${p.name}</td>
            <td>${p.description}</td>
            <td>S/. ${Number(p.price).toFixed(2)}</td>
            <td>${p.stock}</td>
            <td>
                <button class="btn-edit" onclick="editProduct(${p.id}, '${p.name}', '${p.description}', ${p.price}, ${p.stock})">Editar</button>
                <button class="btn-delete" onclick="deleteProduct(${p.id})">Eliminar</button>
            </td>
        `;
        productsTableBody.appendChild(row);
    });
}

// Crear o Actualizar Producto (POST / PUT)
productForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const token = localStorage.getItem("jwt_token");
    const id = document.getElementById("product-id").value;

    const productData = {
        name: document.getElementById("prod-name").value.trim(),
        description: document.getElementById("prod-desc").value.trim(),
        price: parseFloat(document.getElementById("prod-price").value),
        stock: parseInt(document.getElementById("prod-stock").value)
    };

    const method = id ? "PUT" : "POST";
    const url = id ? `${API_URL}/products/${id}` : `${API_URL}/products`;

    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(productData)
        });

        if (response.ok) {
            resetProductForm();
            loadProducts();
        } else {
            alert("Verifica los datos ingresados (precio y stock deben ser positivos).");
        }
    } catch (error) {
        console.error("Error guardando producto:", error);
    }
});

// Preparar formulario para edición
window.editProduct = (id, name, description, price, stock) => {
    document.getElementById("product-id").value = id;
    document.getElementById("prod-name").value = name;
    document.getElementById("prod-desc").value = description;
    document.getElementById("prod-price").value = price;
    document.getElementById("prod-stock").value = stock;

    formTitle.textContent = `Editando Producto #${id}`;
    btnCancelEdit.classList.remove("hidden");
};

btnCancelEdit.addEventListener("click", resetProductForm);

function resetProductForm() {
    productForm.reset();
    document.getElementById("product-id").value = "";
    formTitle.textContent = "Agregar Nuevo Producto";
    btnCancelEdit.classList.add("hidden");
}

// Eliminar Producto (DELETE)
window.deleteProduct = async (id) => {
    if (!confirm("¿Seguro que deseas eliminar este producto?")) return;

    const token = localStorage.getItem("jwt_token");

    try {
        const response = await fetch(`${API_URL}/products/${id}`, {
            method: "DELETE",
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (response.ok) {
            loadProducts();
        }
    } catch (error) {
        console.error("Error eliminando producto:", error);
    }
};

function showMessage(text, color) {
    authMessage.textContent = text;
    authMessage.style.color = color;
}