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
    useSearch,
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
    partCadInfosSearchResults,
    partCadInfosSearchLoading,
    searchPartCadInfos,
    clearPartCadInfosSearch
}) {
    const [partCadInfo, setPartCadInfo] = useState(null);

    const editing = () => editStatus === 'edit';

    useEffect(() => {
        setPartCadInfo(item);
    }, [item]);

    const handleFieldChange = (propertyName, newValue) => {
        setEditStatus('edit');
        setPartCadInfo({ ...partCadInfo, [propertyName]: newValue });
    };

    const handleCancelClick = () => {
        setPartCadInfo(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        setEditStatus('view');
        history.goBack();
    };

    const handleSaveClick = () => {
        updatePartCadInfo(partCadInfo.msId, partCadInfo);
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
                            setPartCadInfo(newValue);
                        }}
                        label="Part"
                        modal
                        items={partCadInfosSearchResults}
                        value={partCadInfo?.partNumber || ''}
                        loading={partCadInfosSearchLoading}
                        fetchItems={searchPartCadInfos}
                        links={false}
                        clearSearch={() => clearPartCadInfosSearch}
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
                        {partCadInfo && (
                            <>
                                <Grid item xs={8}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={partCadInfo.description}
                                        label="Description"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        value={partCadInfo.footprintRef}
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
                                        value={partCadInfo.libraryRef}
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
                                        value={partCadInfo.libraryName}
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
    partCadInfosSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partCadInfosSearchLoading: PropTypes.bool,
    searchPartCadInfos: PropTypes.func.isRequired,
    clearPartCadInfosSearch: PropTypes.func.isRequired
};

PartCadInfo.defaultProps = {
    snackbarVisible: false,
    itemErrors: null,
    loading: false,
    item: null,
    editStatus: 'view',
    partCadInfosSearchResults: [],
    partCadInfosSearchLoading: false
};
