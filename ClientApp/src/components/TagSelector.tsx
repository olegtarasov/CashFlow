import React, {useCallback, useEffect, useState} from "react";
import DropdownTreeSelect, {TreeNode} from "react-dropdown-tree-select";
import "react-dropdown-tree-select/dist/styles.css";
import {useErrorContext} from "../context/errorContext";
import axios from "axios";

interface Tag {
    id: string;
    title: string;
    childrenTags: Tag[];
}

function mapTag(tag: Tag): TreeNode {
    return {id: tag.id, label: tag.title, value: tag.id, children: tag.childrenTags.map(mapTag), checked: true};
}

function updateChecked(tags: TreeNode[], selectedIds: string[]) {
    for (const tag of tags) {
        tag.checked = selectedIds.includes(tag.id);
        if (tag.children.length > 0)
            updateChecked(tag.children, selectedIds);
    }
}

export interface TypeSelectorProps {
    onSelectedChanged: (tags: string[]) => void;
}

export const TagSelector = React.memo(function TagSelector({onSelectedChanged}: TypeSelectorProps) {
    const errorContext = useErrorContext();
    const [tags, setTags] = useState<TreeNode[]>([]);
    const [tagsRequested, setTagsRequested] = useState(false);
    const raiseSelectedChanged = useCallback(function (selected: TreeNode[]) {
        onSelectedChanged(selected.map(x => x.value));
    }, [onSelectedChanged]);

    useEffect(() => {
        if (tagsRequested)
            return;

        axios.get("api/Tags?mode=outcome")
            .then(response => {
                const nodes = response.data.map(mapTag);
                setTags(nodes);
                raiseSelectedChanged(nodes);
            })
            .catch(error => {
                errorContext.processError(error);
            })
            .then(() => setTagsRequested(true));
    }, [raiseSelectedChanged, errorContext, tagsRequested]);

    function onSelectedChange(current: TreeNode, selected: TreeNode[]) {
        const selectedIds = selected.map(x => x.id);
        updateChecked(tags, selectedIds);
        setTags(tags);
        raiseSelectedChanged(selected);
    }

    return (
        <DropdownTreeSelect className="my-2" data={tags}
                                onChange={onSelectedChange}/>
    );
});