import React from "react";

const NewTabLink = ({href, children}) => (
    <a href={href} target="_blank" rel="noopener noreferrer">{children}</a>
);

export default NewTabLink;