// import React from "react";
// import {RadioGroup, Radio, cn} from "@nextui-org/react";
//
// export const CustomRadio = (props) => {
//     const {children, ...otherProps} = props;
//
//     return (
//         <Radio
//             {...otherProps}
//             classNames={{
//                 base: cn(
//                     "size-20 inline-flex m-0 bg-content1 hover:bg-content2 items-center justify-between",
//                     "flex-row-reverse max-w-[300px] cursor-pointer rounded-lg gap-4 p-4 border-2 border-transparent",
//                     "data-[selected=true]:border-primary"
//                 ),
//             }}
//         >
//             {children}
//         </Radio>
//     );
// };
//
// export const DatabaseCards = () => {
//     return (
//         <RadioGroup className="flex flex-row gap-3 w-full" orientation="horizontal">
//             <CustomRadio description="Up to " value="free">
//                 Free
//             </CustomRadio>
//             <CustomRadio description="Unlimit" value="pro">
//                 Pro
//             </CustomRadio>
//             <CustomRadio
//                 description="24/7"
//                 value="enterprise"
//             >
//                 Enter
//             </CustomRadio>
//             <CustomRadio description="Up t" value="free">
//                 Free
//             </CustomRadio>
//         </RadioGroup>
//     );
// }
