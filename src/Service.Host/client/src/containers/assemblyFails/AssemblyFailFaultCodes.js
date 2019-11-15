import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import AssemblyFailFaultCodes from '../../components/assemblyFails/AssemblyFailFaultCodes';
import assemblyFailFaultCodesSelectors from '../../selectors/assemblyFailFaultCodesSelectors';
import assemblyFailFaultCodesActions from '../../actions/assemblyFailFaultCodesActions';
import assemblyFailFaultCodeActions from '../../actions/assemblyFailFaultCodeActions';
import assemblyFailFaultCodeSelectors from '../../selectors/assemblyFailFaultCodeSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: assemblyFailFaultCodesSelectors.getItems(state),
    loading: assemblyFailFaultCodesSelectors.getLoading(state),
    faultCodeLoading: assemblyFailFaultCodeSelectors.getLoading(state),
    snackbarVisible: assemblyFailFaultCodeSelectors.getSnackbarVisible(state),
    faultCodeError: getItemErrorDetailMessage(state, itemTypes.assemblyFailFaultCode.item),
    faultCodesError: getItemErrorDetailMessage(state, itemTypes.assemblyFailFaultCodes.item)
});

const initialise = () => dispatch => {
    dispatch(assemblyFailFaultCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    updateAssemblyFailFaultCode: assemblyFailFaultCodeActions.update,
    setSnackbarVisible: assemblyFailFaultCodeActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AssemblyFailFaultCodes));
