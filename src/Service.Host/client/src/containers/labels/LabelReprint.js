import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import LabelReprint from '../../components/labels/LabelReprint';
import serialNumberActions from '../../actions/serialNumberActions';
import serialNumberSelectors from '../../selectors/serialNumberSelectors';

const mapStateToProps = state => ({
    itemErrors: getItemErrors(state),
    serialNumbers: serialNumberSelectors.getItems(state),
    serialNumbersLoading: serialNumberSelectors.getLoading(state)
});

const mapDispatchToProps = {
    fetchSerialNumbers: serialNumberActions.fetchByQueryString
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(LabelReprint);
