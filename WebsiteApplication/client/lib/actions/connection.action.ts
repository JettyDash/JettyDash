import { toast } from "sonner";
import axios from "axios";

interface testHostConnectionParams {
		HostDatabaseName: string;
		HostDatabaseType: string;
		HostUsername: string;
		HostPassword: string;
		Host: string;
		Port: string;
}


export async function testHostConnectionRequest(params: testHostConnectionParams) {
		const url = "http://localhost:5000/api/testHostConnection";
		const toastId = toast.loading("Loading..."); // Assuming toast is defined somewhere in your code

		try {
				const response = await axios.post(url, params);

				if (response.status === 200) {
						// toast.success(response.statusText, {
						// 		id: toastId,
						// 		position: "top-center"
						// });
						toast.success(`Connection Successful ${JSON.stringify(response.data, null, 2)}`, {
								id: toastId,
								duration: 1000,
								position: "bottom-right"
						});
						return response.data;
				} else {
						toast.error(`HTTP error! status: ${response.status}`, {
								id: toastId,
								duration: 2000,
								position: "bottom-right"
						});

				}
		} catch (error) {
				console.error("Axios error:", error);
				toast.error(`Axios error! status: ${error}`, {
						id: toastId,
						duration: 3000,
						position: "bottom-right"
				});
		}
}

// export async function testHostConnectionRequest(params: testHostConnectionParams) {
// 		const url = "http://localhost:5000/api/testHostConnection";
//
// 		const options = {
// 				method: "POST",
// 				body: JSON.stringify({ ...params })
// 		};
//
// 		try {
// 				const response = await fetch(url, options);
//
// 				if (!response.ok) {
// 						return new Error(`HTTP error! status: ${response.status}`);
// 				}
//
// 				const contentType = response.headers.get("Content-Type");
// 				if (contentType && contentType.includes("application/json")) {
// 						return await response.json(); // Parse JSON response
// 						// console.log(data);
// 				} else {
// 						const text = await response.text(); // Read response as text
// 						console.log("Response is not JSON:", text);
// 						return new Error("Unexpected response format");
// 				}
// 		} catch (error) {
// 				console.error("Fetch error:", error);
// 				throw new Error(`HTTP error! status: ${error}`);
//
// 		}
// }