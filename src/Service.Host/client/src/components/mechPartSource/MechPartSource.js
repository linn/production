import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Title,
    Loading,
    InputField,
    SearchInputField,
    SaveBackCancelButtons,
    SnackbarMessage,
    ErrorCard,
    useSearch
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Page from '../../containers/Page';

export default function MechPartSource({
    loading,
    item,
    snackbarVisible,
    itemErrors,
    fetchMechPartSource,
    updateMechPartSource,
    editStatus,
    setEditStatus,
    history,
    setSnackbarVisible
}) {
    const [mechPartSource, setMechPartSource] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');

    useSearch(fetchMechPartSource, searchTerm, null);

    const editing = () => editStatus === 'edit';

    useEffect(() => {
        setMechPartSource(item);
    }, [item]);

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'searchTerm') {
            setEditStatus('view');
            setSearchTerm(newValue);
        } else {
            setEditStatus('edit');
            setMechPartSource({ ...mechPartSource, [propertyName]: newValue });
        }
    };

    const handleCancelClick = () => {
        setMechPartSource(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        setEditStatus('view');
        history.goBack();
    };

    const handleSaveClick = () => {
        updateMechPartSource(mechPartSource.msId, mechPartSource);
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Mech Part Source" />
                </Grid>

                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Save Succesful"
                />

                {itemErrors &&
                    !loading &&
                    itemErrors?.map(itemError => (
                        <Grid item xs={12}>
                            <ErrorCard
                                errorMessage={`${itemError.item} ${itemError.statusText} - ${itemError.details?.errors?.[0]}`}
                            />
                        </Grid>
                    ))}

                <Grid item xs={4}>
                    <SearchInputField
                        label="Mech Part Source ID"
                        fullWidth
                        placeHolder="Search for Mech Part Source"
                        onChange={handleFieldChange}
                        propertyName="searchTerm"
                        value={searchTerm}
                    />
                </Grid>
                <Grid item xs={8} />

                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {mechPartSource && (
                            <>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={mechPartSource.partNumber}
                                        label="Part Number"
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={8}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={mechPartSource.description}
                                        label="Description"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={mechPartSource.footprintRef}
                                        label="Footprint Ref"
                                        maxLength={30}
                                        onChange={handleFieldChange}
                                        propertyName="footprintRef"
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={mechPartSource.libraryRef}
                                        label="Library Ref"
                                        maxLength={30}
                                        onChange={handleFieldChange}
                                        propertyName="libraryRef"
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={12}>
                                    <SaveBackCancelButtons
                                        saveDisabled={!editing}
                                        saveClick={handleSaveClick}
                                        cancelClick={handleCancelClick}
                                        backClick={handleBackClick}
                                    />
                                </Grid>
                            </>
                        )}
                    </>
                )}
            </Grid>
        </Page>
    );
}

MechPartSource.propTypes = {
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    itemErrors: PropTypes.arrayOf(
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ),
    loading: PropTypes.bool,
    item: PropTypes.shape({}),
    snackbarVisible: PropTypes.bool,
    fetchMechPartSource: PropTypes.func.isRequired,
    updateMechPartSource: PropTypes.func.isRequired,
    editStatus: PropTypes.string,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

MechPartSource.defaultProps = {
    snackbarVisible: false,
    itemErrors: null,
    loading: false,
    item: null,
    editStatus: 'view'
};
