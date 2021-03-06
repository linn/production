import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function BoardFailType({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible
}) {
    const [boardFailType, setBoardFailType] = useState({});
    const [prevBoardFailType, setPrevBoardFailType] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevBoardFailType) {
            setBoardFailType(item);
            setPrevBoardFailType(item);
        }
    }, [item, prevBoardFailType]);

    const failTypeInvalid = () => !boardFailType.failType;
    const descriptionInvalid = () => !boardFailType.description;

    const inputInvalid = () => failTypeInvalid() || descriptionInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, boardFailType);
            setEditStatus('view');
        } else if (creating()) {
            addItem(boardFailType);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setBoardFailType(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/resources/board-fail-types/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setBoardFailType({ ...boardFailType, [propertyName]: newValue });
    };
    if (loading) {
        return (
            <Page showRequestErrors>
                <Grid item xs={12}>
                    <Loading />
                </Grid>
            </Page>
        );
    }
    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Board Fail Type" />
                    ) : (
                        <Title text="Board Fail Type" />
                    )}
                </Grid>
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                ) : (
                    boardFailType && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={boardFailType.failType}
                                    label="Fail Type"
                                    maxLength={10}
                                    helperText={
                                        !creating()
                                            ? 'This field cannot be changed'
                                            : `${failTypeInvalid() ? 'This field is required' : ''}`
                                    }
                                    required={creating()}
                                    onChange={handleFieldChange}
                                    propertyName="failType"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={boardFailType.description}
                                    label="Description"
                                    maxLength={50}
                                    fullWidth
                                    helperText={
                                        descriptionInvalid() ? 'This field is required' : ''
                                    }
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="description"
                                />
                            </Grid>
                        </>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={viewing() || inputInvalid()}
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={handleBackClick}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

BoardFailType.propTypes = {
    item: PropTypes.shape({
        failType: PropTypes.number,
        description: PropTypes.string,
        hourlyRate: PropTypes.number
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string }),
    setSnackbarVisible: PropTypes.func.isRequired
};

BoardFailType.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemId: null,
    itemError: null
};

export default BoardFailType;
