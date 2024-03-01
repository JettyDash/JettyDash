// import React from "react";
//
// export const LoadingIcon: React.FC = () => {
// 		return (
// 				<div className="lds-spinner">
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 						<div></div>
// 				</div>
// 		);
// };
//


import React from "react";

const bars = Array(12).fill(0);

const Loader = ({ visible }: { visible: boolean }) => {
		return (
				<div className="sonner-loading-wrapper" data-visible={visible}>
						<div className="sonner-spinner">
								{bars.map((_, i) => (
										<div className="sonner-loading-bar" key={`spinner-bar-${i}`} />
								))}
						</div>
				</div>
		);
};

export default Loader;

