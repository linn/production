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

function LabelType({
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
    const [labelType, setLabelType] = useState({});
    const [prevLabelType, setPrevLabelType] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevLabelType) {
            setLabelType(item);
            setPrevLabelType(item);
        }
    }, [item, prevLabelType]);

    const labelTypeCodeInvalid = () => !labelType.labelTypeCode;
    const descriptionInvalid = () => !labelType.description;
    const filenameInvalid = () => !labelType.filename;
    const defaultPrinterInvalid = () => !labelType.defaultPrinter;
    const commandFilenameInvalid = () => !labelType.commandFilename;

    const inputInvalid = () =>
        labelTypeCodeInvalid() ||
        descriptionInvalid() ||
        filenameInvalid() ||
        defaultPrinterInvalid() ||
        commandFilenameInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, labelType);
            setEditStatus('view');
        } else if (creating()) {
            addItem(labelType);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setLabelType(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/resources/label-types/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setLabelType({ ...labelType, [propertyName]: newValue });
    };

    return (
        <>
            <Grid container alignItems="center" justify="center">
                <Grid xs={6} item>
                    <Page>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                {creating() ? (
                                    <Title text="Create Label Type" />
                                ) : (
                                    <Title text="Label Type" />
                                )}
                            </Grid>
                            {itemError && (
                                <Grid item xs={12}>
                                    <ErrorCard errorMessage={itemError.statusText} />
                                </Grid>
                            )}
                            {loading ? (
                                <Grid item xs={12}>
                                    <Loading />
                                </Grid>
                            ) : (
                                labelType && (
                                    <>
                                        <SnackbarMessage
                                            visible={snackbarVisible}
                                            onClose={() => setSnackbarVisible(false)}
                                            message="Save Successful"
                                        />
                                        <Grid item xs={12}>
                                            <InputField
                                                fullWidth
                                                disabled={!creating()}
                                                value={labelType.labelTypeCode}
                                                label="Label Type Code"
                                                maxLength={16}
                                                helperText={
                                                    !creating()
                                                        ? 'This field cannot be changed'
                                                        : `${
                                                              labelTypeCodeInvalid()
                                                                  ? 'This field is required'
                                                                  : ''
                                                          }`
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="labelTypeCode"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.description}
                                                label="Description"
                                                maxLength={50}
                                                fullWidth
                                                helperText={
                                                    descriptionInvalid()
                                                        ? 'This field is required'
                                                        : ''
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="description"
                                                rows={2}
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Grid item xs={2}>
                                                <InputField
                                                    value={labelType.barcodePrefix}
                                                    label="Barcode Prefix"
                                                    maxLength={2}
                                                    fullWidth
                                                    onChange={handleFieldChange}
                                                    propertyName="barcodePrefix"
                                                />
                                            </Grid>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Grid item xs={2}>
                                                <InputField
                                                    value={labelType.nSBarcodePrefix}
                                                    label="NSBarcodePrefix"
                                                    maxLength={2}
                                                    fullWidth
                                                    onChange={handleFieldChange}
                                                    propertyName="nSBarcodePrefix"
                                                />
                                            </Grid>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.filename}
                                                label="Filename"
                                                maxLength={50}
                                                fullWidth
                                                helperText={
                                                    filenameInvalid()
                                                        ? 'This field is required'
                                                        : ''
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="filename"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.defaultPrinter}
                                                label="Default Printer"
                                                maxLength={50}
                                                fullWidth
                                                helperText={
                                                    defaultPrinterInvalid()
                                                        ? 'This field is required'
                                                        : ''
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="defaultPrinter"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.commandFilename}
                                                label="Command Filename"
                                                maxLength={50}
                                                fullWidth
                                                helperText={
                                                    commandFilenameInvalid()
                                                        ? 'This field is required'
                                                        : ''
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="commandFilename"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.testFilename}
                                                label="Test Filename"
                                                maxLength={50}
                                                fullWidth
                                                onChange={handleFieldChange}
                                                propertyName="testFilename"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.testPrinter}
                                                label="Test Printer"
                                                maxLength={50}
                                                fullWidth
                                                onChange={handleFieldChange}
                                                propertyName="testPrinter"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelType.testCommandFilename}
                                                label="Test Command Filename"
                                                maxLength={50}
                                                fullWidth
                                                onChange={handleFieldChange}
                                                propertyName="testCommandFilename"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <SaveBackCancelButtons
                                                saveDisabled={viewing() || inputInvalid()}
                                                saveClick={handleSaveClick}
                                                cancelClick={handleCancelClick}
                                                backClick={handleBackClick}
                                            />
                                        </Grid>
                                    </>
                                )
                            )}
                        </Grid>
                    </Page>
                </Grid>
            </Grid>
        </>
    );
}

LabelType.propTypes = {
    item: PropTypes.shape({
        skillCode: PropTypes.string,
        description: PropTypes.string,
        hourlyRate: PropTypes.number
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

LabelType.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default LabelType;
