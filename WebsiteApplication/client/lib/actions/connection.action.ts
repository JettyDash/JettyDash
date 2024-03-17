import { toast } from "sonner";
import axios, { AxiosError, isAxiosError } from "axios";

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

						let content = JSON.stringify(response.data, null, 2);
						if (content.length > 200) {
								content = content.substring(0, 197) + "...";
						}

						toast.success("Connection Successful", {
								id: toastId,
								duration: 2000,
								position: "bottom-right",
								description: content
						});
						return response.data;
				} else {
						toast.error(`HTTP error! status: ${response.status}`, {
								id: toastId,
								duration: 2000,
								position: "bottom-right"
						});

				}
		} catch (error: AxiosError | any) {
				let content: string = "Unknown error occurred";
				let statusCode: number = 500;
				if (isAxiosError(error)) {
						statusCode = error.response?.status || 500;
						let jsonContent = error.response?.data || error.message;
						// if content length is greater than 100, truncate it
						content = JSON.stringify(jsonContent, null, 2);
						if (content.length > 200) {
								content = content.substring(0, 197) + "...";
						}
				} else {
						content = String(error); // Convert unknown to string
				}

				toast.error(`Error ${statusCode}`, {
						id: toastId,
						duration: 3000,
						position: "bottom-right",
						description: content
				});
		}
}


