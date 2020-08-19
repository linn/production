import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Title,
    Loading,
    InputField,
    SaveBackCancelButtons,
    SnackbarMessage,
    ErrorCard,
    Typeahead
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Page from '../../containers/Page';

export default function PartCadInfo({
    loading,
    item,
    snackbarVisible,
    itemErrors,
    updatePartCadInfo,
    editStatus,
    setEditStatus,
    history,
    setSnackbarVisible,
    partsSearchResults,
    partsSearchLoading,
    searchParts,
    clearPartsSearch
}) {
    const [part, setPart] = useState(null);

    const editing = () => editStatus === 'edit';

    useEffect(() => {
        setPart(item);
    }, [item]);

    const handleFieldChange = (propertyName, newValue) => {
        if (editStatus !== 'edit') {
            setEditStatus('edit');
        }
        setPart({ ...part, [propertyName]: newValue });
    };

    const handleCancelClick = () => {
        setPart(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        setEditStatus('view');
        history.goBack();
    };

    const handleSaveClick = () => {
        clearPartsSearch();
        updatePartCadInfo(part.partNumber, part);
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Part Cad Info" />
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
                                errorMessage={`${itemError.item} ${
                                    itemError.statusText
                                } - ${itemError.details?.errors?.[0] || ''}`}
                            />
                        </Grid>
                    ))}

                <Grid item xs={4}>
                    <Typeahead
                        onSelect={newValue => {
                            setEditStatus('edit');
                            setPart(newValue);
                        }}
                        label="Part"
                        modal
                        items={partsSearchResults}
                        value={part?.partNumber || ''}
                        loading={partsSearchLoading}
                        fetchItems={searchParts}
                        links={false}
                        clearSearch={() => clearPartsSearch}
                        placeholder="Search By Part Number"
                    />
                </Grid>
                <Grid item xs={8} />

                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        {part && (
                            <>
                                <Grid item xs={8}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={part.description}
                                        label="Description"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={part.footprintRef}
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
                                        value={part.libraryRef}
                                        label="Library Ref"
                                        maxLength={30}
                                        onChange={handleFieldChange}
                                        propertyName="libraryRef"
                                    />
                                </Grid>
                                <Grid item xs={8} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={part.libraryName}
                                        label="Library Name"
                                        maxLength={30}
                                        onChange={handleFieldChange}
                                        propertyName="libraryName"
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

PartCadInfo.propTypes = {
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
    updatePartCadInfo: PropTypes.func.isRequired,
    editStatus: PropTypes.string,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired
};

PartCadInfo.defaultProps = {
    snackbarVisible: false,
    itemErrors: null,
    loading: false,
    item: null,
    editStatus: 'view',
    partsSearchResults: [],
    partsSearchLoading: false
};
