import { connect } from 'react-redux';
import { getItemErrorDetailMessage } from '@linn-it/linn-form-components-library';
import AssemblyFailFaultCode from '../../components/assemblyFails/AssemblyFailFaultCode';
import assemblyFailFaultCodeSelectors from '../../selectors/assemblyFailFaultCodeSelectors';
import assemblyFailFaultCodeActions from '../../actions/assemblyFailFaultCodeActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    error: getItemErrorDetailMessage(state, itemTypes.assemblyFailFaultCode.item),
    editStatus: 'create',
    loading: assemblyFailFaultCodeSelectors.getLoading(state),
    snackbarVisible: assemblyFailFaultCodeSelectors.getSnackbarVisible(state)
});

const mapDispatchToProps = {
    addItem: assemblyFailFaultCodeActions.add,
    setSnackbarVisible: assemblyFailFaultCodeActions.setSnackbarVisible,
    setEditStatus: assemblyFailFaultCodeActions.setEditStatus,
    updateItem: assemblyFailFaultCodeActions.update
};

export default connect(mapStateToProps, mapDispatchToProps)(AssemblyFailFaultCode);
