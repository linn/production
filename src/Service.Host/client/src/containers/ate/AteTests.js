import { connect } from 'react-redux';
import AteTests from '../../components/ate/AteTests';
import ateTestsActions from '../../actions/ateTestsActions';
import ateTestsSelectors from '../../selectors/ateTestsSelectors';

const mapStateToProps = state => ({
    items: ateTestsSelectors.getSearchItems(state),
    loading: ateTestsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: ateTestsActions.search,
    clearSearch: ateTestsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(AteTests);
