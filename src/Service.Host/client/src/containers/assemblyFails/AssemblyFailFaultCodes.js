import { connect } from 'react-redux';
import { getItemErrorDetailMessage } from '@linn-it/linn-form-components-library';
import initialiseOnMount from '../initialiseOnMount';
import AssemblyFailFaultCodes from '../../components/assemblyFails/AssemblyFailFaultCodes';
import assemblyFailFaultCodesSelectors from '../../selectors/assemblyFailFaultCodesSelectors';
import assemblyFailFaultCodesActions from '../../actions/assemblyFailFaultCodesActions';

const mapStateToProps = state => ({
    items: assemblyFailFaultCodesSelectors.getItems(state),
    loading: assemblyFailFaultCodesSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(assemblyFailFaultCodesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AssemblyFailFaultCodes));
