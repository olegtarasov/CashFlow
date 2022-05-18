import React, {useEffect, useState} from "react";
import DropdownTreeSelect from "react-dropdown-tree-select";
import "react-dropdown-tree-select/dist/styles.css";
import PropTypes from "prop-types";
import {useErrorContext} from "../context/errorContext";
import axios from "axios";

function mapTag(tag) {
    return {label: tag.title, value: tag.id, children: tag.childrenTags.map(mapTag), checked: true};
}

function TagSelector({onChange}) {
    const errorContext = useErrorContext();
    const [tags, setTags] = useState({});
    const [tagsRequested, setTagsRequested] = useState(false);

    useEffect(() => {
        if (tagsRequested)
            return;

        axios.get("api/Tags?mode=outcome")
            .then(response => {
                const nodes = response.data.map(mapTag);
                setTags(nodes);
                onSelectedChange(null, nodes);
            })
            .catch(error => {
                errorContext.processError(error);
            })
            .then(() => setTagsRequested(true));
    });

    function onSelectedChange(current, selected) {
        onChange(selected.map(x => x.value));
    }

    return (
        <DropdownTreeSelect className="my-2" data={tags}
                            onChange={onSelectedChange}/>
    );
}

TagSelector.propTypes = {
    onChange: PropTypes.func.isRequired
}

export {TagSelector};