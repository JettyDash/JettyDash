import React from 'react';

type IconProps = React.SVGProps<SVGSVGElement>;

const DeleteIcon = (props: IconProps) => (
    <svg
        aria-hidden='true'
        fill='none'
        focusable='false'
        height='1em'
        role='presentation'
        viewBox='0 0 20 20'
        width='1em'
        {...props}
    >
        {/* Path elements */}
    </svg>
);

export default DeleteIcon;
